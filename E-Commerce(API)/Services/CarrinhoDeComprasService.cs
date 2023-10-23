using AutoMapper;
using E_Commerce_API_.Constants;
using E_Commerce_API_.Data.DTOs.CarrinhoDeComprasDto;
using E_Commerce_API_.Data.DTOs.ProdutoDto;
using E_Commerce_API_.Exceptions;
using E_Commerce_API_.Interfaces;
using E_Commerce_API_.Models;
using System.Text.Json;

namespace E_Commerce_API_.Services;

public class CarrinhoDeComprasService : ICarrinhoDeComprasService
{
    private readonly IProdutoService _produtoService;
    private readonly ICarrinhoDeComprasDAO _carrinhoDeComprasDAO;
    private readonly IMapper _mapper;
    private readonly IGeradorDeCupomService _cupomDeDescontoService;

    public CarrinhoDeComprasService(IProdutoService produtoService, IMapper mapper, ICarrinhoDeComprasDAO carrinhoDeComprasDAO, IGeradorDeCupomService cupomDeDescontoService)
    {
        _produtoService = produtoService;
        _mapper = mapper;
        _carrinhoDeComprasDAO = carrinhoDeComprasDAO;
        _cupomDeDescontoService = cupomDeDescontoService;
    }

    public async Task<ReadCarrinhoDeComprasDto> CriarCarrinho(CreateCarrinhoDeComprasDto create)
    {
        var produto = _produtoService.PesquisarProdutoId(create.ProdutoId);

        VerificaEstoqueEStatus(create.Quantidade, produto);

        var carrinho = new CarrinhoDeCompras();

        var produtoNoCarrinho = new ProdutoNoCarrinho(carrinho.Id, produto.Id, create.Quantidade, produto.Valor);
        carrinho.Produtos = new List<ProdutoNoCarrinho>() { produtoNoCarrinho };

        _carrinhoDeComprasDAO.Create(carrinho);

        return ReadCarrinhoDeCompras(carrinho);
    }

    private static void VerificaEstoqueEStatus(uint quantidade, ReadProdutoDto produto)
    {
        if (produto.QuantidadeEmEstoque < 1 || produto.QuantidadeEmEstoque < quantidade)
            throw new BadRequestException(ErrorMessages.ProdutoSemEstoque);

        if (produto.Status == false)
            throw new BadRequestException(ErrorMessages.ProdutoInativo);
    }

    public ReadCarrinhoDeComprasDto AdicionarProdutosAoCarrinho(Guid carrinhoDeComprasId, AddProdutosAoCarrinhoDto addDto)
    {
        var carrinho = _carrinhoDeComprasDAO.GetCarrinhoDeCompras(carrinhoDeComprasId);

        if (carrinho.Produtos.Any(p => p.ProdutoId == addDto.ProdutoId))
            throw new BadRequestException(ErrorMessages.ProdutoConstaNoCarrinho);

        var produto = _produtoService.PesquisarProdutoId(addDto.ProdutoId);

        VerificaEstoqueEStatus(addDto.Quantidade, produto);

        var produtoNoCarrinho = new ProdutoNoCarrinho(carrinho.Id, produto.Id, addDto.Quantidade, produto.Valor);

        _carrinhoDeComprasDAO.AddProdutoAoCarrinho(produtoNoCarrinho);

        return ReadCarrinhoDeCompras(carrinho);
    }

    public ReadCarrinhoDeComprasDto AlterarQuantidadeDeProdutosNoCarrinho(Guid carrinhoDeComprasId, Guid produtoId, UpdateCarrinhoDeComprasDto updateDto)
    {
        var carrinho = _carrinhoDeComprasDAO.GetCarrinhoDeCompras(carrinhoDeComprasId);

        var produtoNoCarrinho = _carrinhoDeComprasDAO
                                .GetProdutosNoCarrinho(carrinhoDeComprasId)
                                .FirstOrDefault(p => p.ProdutoId == produtoId);

        if (produtoNoCarrinho == null)
            throw new NotFoundException(ErrorMessages.ProdutoNaoConstaNoCarrinho);

        if (produtoNoCarrinho.Quantidade == updateDto.Quantidade)
            throw new BadRequestException(ErrorMessages.QuantidadeNaoAlterada);

        if (updateDto.Quantidade == 0)
        {
            _carrinhoDeComprasDAO.RemoverProdutoDoCarrinho(produtoNoCarrinho);
            return ReadCarrinhoDeCompras(carrinho);
        }

        var produto = _produtoService.PesquisarProdutoId(produtoId);

        VerificaEstoqueEStatus(updateDto.Quantidade, produto);

        produtoNoCarrinho.Quantidade = updateDto.Quantidade;

        _carrinhoDeComprasDAO.AtualizarProdutoNoCarrinho(produtoNoCarrinho);

        return ReadCarrinhoDeCompras(carrinho);
    }

    public ReadCarrinhoDeComprasDto GetCarrinho(Guid carrinhoDeComprasId)
    {
        var carrinho = _carrinhoDeComprasDAO.GetCarrinhoDeCompras(carrinhoDeComprasId);

        return ReadCarrinhoDeCompras(carrinho);
    }

    private ReadCarrinhoDeComprasDto ReadCarrinhoDeCompras(CarrinhoDeCompras carrinho)
    {
        var listaDeProdutos = new List<ResumoDoProduto>();

        var produtosNoCarrinho = _carrinhoDeComprasDAO.GetProdutosNoCarrinho(carrinho.Id);

        foreach (var item in produtosNoCarrinho)
        {
            listaDeProdutos.Add(new ResumoDoProduto
            {
                Id = item.ProdutoId,
                Nome = item.Produto.Nome,
                ValorUnitario = item.Produto.Valor.ToString("C"),
                Quantidade = item.Quantidade,
                Atencao = MensagemDeAtencao(item)
            });
        }

        var readCarrinhoDeCompras = _mapper.Map<ReadCarrinhoDeComprasDto>(carrinho);
        readCarrinhoDeCompras.Produtos.AddRange(listaDeProdutos);

        return readCarrinhoDeCompras;
    }

    private string? MensagemDeAtencao(ProdutoNoCarrinho produtoNoCarrinho)
    {

        if (produtoNoCarrinho.Produto.QuantidadeEmEstoque < 1)
        {
            _carrinhoDeComprasDAO.RemoverProdutoDoCarrinho(produtoNoCarrinho);

            return ErrorMessages.ProdutoNaoEstaMaisDisponivel;
        }

        if (produtoNoCarrinho.Produto.QuantidadeEmEstoque < produtoNoCarrinho.Quantidade)
        {
            produtoNoCarrinho.Quantidade = (uint)produtoNoCarrinho.Produto.QuantidadeEmEstoque;

            _carrinhoDeComprasDAO.AtualizarProdutoNoCarrinho(produtoNoCarrinho);

            return $"O produto não possui o estoque disponível. A quantidade será ajustada para a quantidade disponível de {produtoNoCarrinho.Produto.QuantidadeEmEstoque} itens.";
        }

        if (produtoNoCarrinho.Produto.Status == false)
        {
            _carrinhoDeComprasDAO.RemoverProdutoDoCarrinho(produtoNoCarrinho);

            return ErrorMessages.ProdutoInativoNoCarrinho;
        }

        if (produtoNoCarrinho.ValorNoCarrinho > produtoNoCarrinho.Produto.Valor)
        {
            string message = $"O preço da unidade foi reduzido em {produtoNoCarrinho.ValorNoCarrinho - produtoNoCarrinho.Produto.Valor:C}.";

            produtoNoCarrinho.ValorNoCarrinho = produtoNoCarrinho.Produto.Valor;

            _carrinhoDeComprasDAO.AtualizarProdutoNoCarrinho(produtoNoCarrinho);
            return message;
        }

        if (produtoNoCarrinho.ValorNoCarrinho < produtoNoCarrinho.Produto.Valor)
        {
            string message = $"O preço da unidade aumentou em {produtoNoCarrinho.Produto.Valor - produtoNoCarrinho.ValorNoCarrinho:C}.";

            produtoNoCarrinho.ValorNoCarrinho = produtoNoCarrinho.Produto.Valor;

            _carrinhoDeComprasDAO.AtualizarProdutoNoCarrinho(produtoNoCarrinho);

            return message;
        }

        return null;
    }

    private async Task<Endereco> EnderecoPorCep(string cep)
    {
        using (HttpClient client = new HttpClient())
        {
            var resposta = await client.GetStringAsync($"https://viacep.com.br/ws/{cep}/json/");
            var endereco = JsonSerializer.Deserialize<Endereco>(resposta)!;

            return endereco.Erro == false
                                    ? endereco
                                    : throw new BadRequestException(ErrorMessages.CEPInvalido);
        }
    }

    public void ExcluirCarrinho(Guid carrinhoDeComprasId)
    {
        var carrinho = _carrinhoDeComprasDAO.GetCarrinhoDeCompras(carrinhoDeComprasId);

        _carrinhoDeComprasDAO.ExcluirCarrinhoDeCompras(carrinho);
    }

    public object RemoverProdutoDoCarrinho(Guid carrinhoDeComprasId, Guid produtoId)
    {
        var produtoNoCarrinho = _carrinhoDeComprasDAO
                                .GetProdutosNoCarrinho(carrinhoDeComprasId)
                                .FirstOrDefault(carrinho => carrinho.ProdutoId == produtoId);

        if (produtoNoCarrinho == null)
            throw new BadRequestException("O produto não está cadastrado no carrinho.");

        _carrinhoDeComprasDAO.RemoverProdutoDoCarrinho(produtoNoCarrinho);

        var carrinho = _carrinhoDeComprasDAO.GetCarrinhoDeCompras(carrinhoDeComprasId);

        return ReadCarrinhoDeCompras(carrinho);
    }

    public async Task<ReadCarrinhoDeComprasDto> AddEnderecoDeEntregaAoCarrinho(Guid carrinhoDeComprasId, AddEnderecoDeEntregaDto enderecoDto)
    {
        var carrinho = _carrinhoDeComprasDAO.GetCarrinhoDeCompras(carrinhoDeComprasId);

        var endereco = await EnderecoPorCep(enderecoDto.CEP);

        _mapper.Map(endereco, carrinho);
        _mapper.Map(enderecoDto, carrinho);

        _carrinhoDeComprasDAO.AtualizarCarrinhoDeCompras(carrinho);

        return ReadCarrinhoDeCompras(carrinho);

    }

    public ReadCarrinhoDeComprasDto InserirCupomDeDesconto(Guid carrinhoDeComprasId, string cupom)
    {
        var carrinho = _carrinhoDeComprasDAO.GetCarrinhoDeCompras(carrinhoDeComprasId);

        var cupomDeDesconto = _cupomDeDescontoService.GetCupomDeDesconto(cupom);

        if (cupomDeDesconto.Uso == nameof(TipoDeUso.Unico) && _carrinhoDeComprasDAO.GetCarrinhosDeCompras().Any(carrinho => carrinho.CupomDeDescontoId == cupomDeDesconto.Id))
            throw new BadRequestException("Cupom já utilizado.");

        if (cupomDeDesconto.Status == false)
            throw new BadRequestException("Cupom inválido.");

        carrinho.CupomDeDescontoId = cupomDeDesconto.Id;

        _carrinhoDeComprasDAO.AtualizarCarrinhoDeCompras(carrinho);

        return ReadCarrinhoDeCompras(carrinho);
    }

    public ReadCarrinhoDeComprasDto RemoverCupomDeDesconto(Guid carrinhoDeComprasId)
    {
        var carrinho = _carrinhoDeComprasDAO.GetCarrinhoDeCompras(carrinhoDeComprasId);

        carrinho.CupomDeDescontoId = null;

        _carrinhoDeComprasDAO.AtualizarCarrinhoDeCompras(carrinho);

        return ReadCarrinhoDeCompras(carrinho);
    }
}

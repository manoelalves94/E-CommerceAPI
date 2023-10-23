using AutoMapper;
using E_Commerce_API_.Models;
using E_Commerce_API_.Data.DTOs.ProdutoDto;
using E_Commerce_API_.Interfaces;
using Serilog;
using E_Commerce_API_.Exceptions;

namespace E_Commerce_API_.Services;

public class ProdutoService : IProdutoService
{
    private IMapper _mapper;
    private IProdutoDAO _iProdutoDAO;
    private readonly ICategoriaDAO _iCategoriaDAO;
    private readonly ISubcategoriaDAO _iSubcategoriaDAO;
    private readonly ICentroDeDistribuicaoDAO _iCentroDeDistribuicaoDAO;


    public ProdutoService(IMapper mapper, IProdutoDAO iProdutoDao, ICategoriaDAO iCategoriaDAO, ISubcategoriaDAO iSubcategoriaDAO, ICentroDeDistribuicaoDAO iCentroDeDistribuicaoDAO)
    {
        _mapper = mapper;
        _iProdutoDAO = iProdutoDao;
        _iCategoriaDAO = iCategoriaDAO;
        _iSubcategoriaDAO = iSubcategoriaDAO;
        _iCentroDeDistribuicaoDAO = iCentroDeDistribuicaoDAO;
    }

    public List<ReadProdutoDto> ListaProdutos(FiltroProdutoDto filtro)
    {
        Log.Information("Iniciada a pesquisa de produtos com os filtros informados: {@filtro}", filtro);

        return _iProdutoDAO.GetListaProdutos(filtro);
    }

    public Produto? BuscarProdutoPorId(Guid id)
    {
        var produto = _iProdutoDAO.GetProdutos().FirstOrDefault(p => p.Id == id);
        if (produto == null)
            throw new NotFoundException($"O produto de Id '{id}' não existe.");
        return produto;
    }

    public ReadProdutoDto AdicionarProduto(CreateProdutoDto produtoDto)
    {
        Log.Information("Iniciada adição de um novo produto.");

        CadastroValido(produtoDto);

        var produto = _mapper.Map<Produto>(produtoDto);

        _iProdutoDAO.Add(produto);

        Log.Information("Produto cadastrado com sucesso.");
        return _mapper.Map<ReadProdutoDto>(produto);
    }

    private void CadastroValido(CreateProdutoDto produtoDto)
    {
        var subcategoria = _iSubcategoriaDAO.GetSubcategorias().FirstOrDefault(s => s.Id == produtoDto.SubcategoriaId);

        if (subcategoria == null)
            throw new NotFoundException($"O Id {produtoDto.SubcategoriaId} de subcategoria informado não existe.");

        if (subcategoria.Status == false)
            throw new BadRequestException($"A subcategoria '{subcategoria.Nome}' está inativa e não pode receber novos cadastros de produtos.");

        if (NomeRepetido(produtoDto.Nome))
            throw new BadRequestException("O nome cadastrado já existe.");

        if (!ValoresValidos(produtoDto))
            throw new BadRequestException("Valores informados para cadastro de produto não são válidos.");
    }

    public void AtualizarProduto(Guid id, UpdateProdutoDto produtoDto)
    {
        Log.Information("Iniciada edição do produto '{@id}'.", id);

        var produto = BuscarProdutoPorId(id);
        if (produto == null) throw new NotFoundException("Id de produto informado para edição não existe.");

        EdicaoValida(produtoDto, produto);

        _iProdutoDAO.Update(produtoDto, produto);

        Log.Information("Produto editado com sucesso.");
    }

    private void EdicaoValida(UpdateProdutoDto produtoDto, Produto produto)
    {
        if (!ValoresAlterados(produtoDto, produto))
            throw new BadRequestException("A edição não foi feita, pois os valores não foram alterados.");

        if (NomeRepetido(produtoDto.Nome, produto.Id))
            throw new BadRequestException("O nome cadastrado já existe.");
    }

    public void RemoverProduto(Guid id)
    {
        Log.Information("Iniciada remoção do produto {@id}.", id);

        var produto = BuscarProdutoPorId(id);
        if (produto == null) throw new NotFoundException("Id de produto informado para remoção não existe.");

        _iProdutoDAO.Delete(produto);

        Log.Information("Produto excluído com sucesso.");
    }

    public ReadProdutoDto PesquisarProdutoId (Guid id)
    {
        Log.Information("Iniciada pesquisa do produto {@id}.", id);

        var produto = BuscarProdutoPorId(id);
        if (produto == null) throw new NotFoundException("Id de produto informado não existe.");

        var produtoDto = _mapper.Map<ReadProdutoDto>(produto);

        return produtoDto;
    }

    private bool NomeRepetido(string nome, Guid? id = null)
    {
        var existeCategoria = _iCategoriaDAO.GetCategorias().Any(c => c.Nome == nome);

        var existeSubcategoria = _iSubcategoriaDAO.GetSubcategorias().Any(s => s.Nome == nome);

        var existeProduto = _iProdutoDAO.GetProdutos().Any(p => p.Nome == nome && p.Id != id);

        var existeCD = _iCentroDeDistribuicaoDAO.GetCentros().Any(cd => cd.Nome == nome);

        return existeCategoria || existeSubcategoria || existeProduto || existeCD;
    }

    public bool ValoresValidos(CreateProdutoDto produtoDto)
    {
        if (produtoDto.Peso <= 0) return false;

        if (produtoDto.Altura <= 0) return false;

        if (produtoDto.Largura <= 0) return false;

        if (produtoDto.Comprimento <= 0) return false;

        if (produtoDto.Valor <= 0) return false;

        if (produtoDto.QuantidadeEmEstoque <= 0) return false;

        return true;
    }

    public bool ValoresAlterados(UpdateProdutoDto produtoDto, Produto produto)
    {
        return !(produtoDto.Nome == produto.Nome &&
        produtoDto.Descricao == produto.Descricao &&
        produtoDto.Peso == produto.Peso &&
        produtoDto.Altura == produto.Altura &&
        produtoDto.Largura == produto.Largura &&
        produtoDto.Comprimento == produto.Comprimento &&
        produtoDto.Valor == produto.Valor &&
        produtoDto.QuantidadeEmEstoque == produto.QuantidadeEmEstoque &&
        produtoDto.Status == produto.Status);
    }

    public List<Produto> BuscarProdutosPorId(List<Guid> produtoId)
    {
        var lista = new List<Produto>();

        foreach (var id in produtoId)
        {
            lista.Add(BuscarProdutoPorId(id));
        }

        return lista;
    }
}

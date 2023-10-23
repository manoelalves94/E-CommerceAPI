using E_Commerce_API_.Data.DTOs.CarrinhoDeComprasDto;

namespace E_Commerce_API_.Interfaces;

public interface ICarrinhoDeComprasService
{
    Task<ReadCarrinhoDeComprasDto> AddEnderecoDeEntregaAoCarrinho(Guid carrinhoDeComprasId, AddEnderecoDeEntregaDto enderecoDto);
    ReadCarrinhoDeComprasDto AdicionarProdutosAoCarrinho(Guid carrinhoDeComprasId, AddProdutosAoCarrinhoDto addDto);
    ReadCarrinhoDeComprasDto AlterarQuantidadeDeProdutosNoCarrinho(Guid carrinhoDeComprasId, Guid produtoId, UpdateCarrinhoDeComprasDto updateDto);
    Task<ReadCarrinhoDeComprasDto> CriarCarrinho(CreateCarrinhoDeComprasDto create);
    void ExcluirCarrinho(Guid carrinhoDeComprasId);
    ReadCarrinhoDeComprasDto GetCarrinho(Guid carrinhoDeComprasId);
    ReadCarrinhoDeComprasDto InserirCupomDeDesconto(Guid carrinhoDeComprasId, string cupom);
    ReadCarrinhoDeComprasDto RemoverCupomDeDesconto(Guid carrinhoDeComprasId);
    object RemoverProdutoDoCarrinho(Guid carrinhoDeComprasId, Guid produtoId);
}

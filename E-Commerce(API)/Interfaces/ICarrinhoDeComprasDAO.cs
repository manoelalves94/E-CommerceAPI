using E_Commerce_API_.Models;

namespace E_Commerce_API_.Interfaces;

public interface ICarrinhoDeComprasDAO
{
    void Create(CarrinhoDeCompras carrinho);
    CarrinhoDeCompras GetCarrinhoDeCompras(Guid carrinhoDeComprasId);
    List<ProdutoNoCarrinho> GetProdutosNoCarrinho(Guid id);
    void RemoverProdutoDoCarrinho(ProdutoNoCarrinho produtoNoCarrinho);
    void AddProdutoAoCarrinho(ProdutoNoCarrinho produtoNoCarrinho);
    void AtualizarCarrinhoDeCompras(CarrinhoDeCompras carrinho);
    void AtualizarProdutoNoCarrinho(ProdutoNoCarrinho produtosNoCarrinho);
    void ExcluirCarrinhoDeCompras(CarrinhoDeCompras carrinho);
    List<CarrinhoDeCompras> GetCarrinhosDeCompras();
}

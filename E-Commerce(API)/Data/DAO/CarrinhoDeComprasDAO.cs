using Dapper;
using E_Commerce_API_.Constants;
using E_Commerce_API_.Exceptions;
using E_Commerce_API_.Interfaces;
using E_Commerce_API_.Models;

namespace E_Commerce_API_.Data.DAO;

public class CarrinhoDeComprasDAO : ICarrinhoDeComprasDAO
{
    private readonly ECommerceContext _context;

    public CarrinhoDeComprasDAO(ECommerceContext context)
    {
        _context = context;
    }

    public void Create(CarrinhoDeCompras carrinho)
    {
        _context.CarrinhosDeCompras.Add(carrinho);
        _context.SaveChanges();
    }

    public CarrinhoDeCompras GetCarrinhoDeCompras(Guid carrinhoDeComprasId)
    {
        var carrinho = _context.CarrinhosDeCompras.FirstOrDefault(c => c.Id == carrinhoDeComprasId);
        if (carrinho == null)
            throw new NotFoundException(ErrorMessages.CarrinhoNaoExiste);

        return carrinho;
    }

    public List<ProdutoNoCarrinho> GetProdutosNoCarrinho(Guid carrinhoId)
    {
        var produtos = _context.ProdutosNoCarrinho.Where(c => c.CarrinhoDeComprasId == carrinhoId).AsList();
        if (produtos == null)
            throw new NotFoundException(ErrorMessages.CarrinhoVazio);

        return produtos;
    }

    public void RemoverProdutoDoCarrinho(ProdutoNoCarrinho produtoNoCarrinho)
    {
        _context.ProdutosNoCarrinho.Remove(produtoNoCarrinho);
        _context.SaveChanges();
    }

    public void AddProdutoAoCarrinho(ProdutoNoCarrinho produtoNoCarrinho)
    {
        _context.ProdutosNoCarrinho.Add(produtoNoCarrinho);
        _context.SaveChanges();
    }

    public void AtualizarCarrinhoDeCompras(CarrinhoDeCompras carrinho)
    {
        _context.CarrinhosDeCompras.Update(carrinho);
        _context.SaveChanges();
    }

    public void AtualizarProdutoNoCarrinho(ProdutoNoCarrinho produto)
    {
        _context.ProdutosNoCarrinho.Update(produto);
        _context.SaveChanges();
    }

    public void ExcluirCarrinhoDeCompras(CarrinhoDeCompras carrinho)
    {
        _context.CarrinhosDeCompras.Remove(carrinho);
        _context.SaveChanges();
    }

    public List<CarrinhoDeCompras> GetCarrinhosDeCompras()
    {
        return _context.CarrinhosDeCompras.ToList();
    }
}

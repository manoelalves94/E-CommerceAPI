namespace E_Commerce_API_.Models;

public class ProdutoNoCarrinho
{
    public ProdutoNoCarrinho(Guid carrinhoDeComprasId, Guid produtoId, uint quantidade, double valorNoCarrinho)
    {
        CarrinhoDeComprasId = carrinhoDeComprasId;
        ProdutoId = produtoId;
        Quantidade = quantidade;
        ValorNoCarrinho = valorNoCarrinho;
    }

    public Guid CarrinhoDeComprasId { get; set; }
    public Guid ProdutoId { get; set; }
    public uint Quantidade { get; set; }
    public double ValorNoCarrinho { get; set; }
    public virtual CarrinhoDeCompras CarrinhoDeCompras { get; set; }
    public virtual Produto Produto { get; set; }
}

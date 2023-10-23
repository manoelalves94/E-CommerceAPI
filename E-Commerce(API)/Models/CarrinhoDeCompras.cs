using System.ComponentModel.DataAnnotations;

namespace E_Commerce_API_.Models;

public class CarrinhoDeCompras
{
    public CarrinhoDeCompras()
    {
        Id = Guid.NewGuid();
    }

    [Key]
    public Guid Id { get; set; }
    public double Subtotal { get => SubtotalDoCarrinho(); }
    public double Total { get => TotalDoCarrinho(); }
    public Guid? CupomDeDescontoId { get; set; }
    public string? Logradouro { get; set; }
    public uint? Numero { get; set; }
    public string? Complemento { get; set; }
    public string? Bairro { get; set; }
    public string? Cidade { get; set; }
    public string? UF { get; set; }
    public string? CEP { get; set; }
    public virtual IEnumerable<ProdutoNoCarrinho> Produtos { get; set; }
    public virtual CupomDeDesconto CupomDeDesconto { get; set; }

    private double SubtotalDoCarrinho()
    {
        double _subtotal = 0;
        var produtos = this.Produtos.ToList();
        foreach (var produto in produtos)
        {
            _subtotal += produto.Produto.Valor * produto.Quantidade;
        }
        return _subtotal;
    }
    private double TotalDoCarrinho()
    {
        if (CupomDeDescontoId == null)
            return Subtotal;

        if (CupomDeDesconto.SetorDeDesconto != Guid.Empty)
        {
            double _subtotal = 0;
            var _produtosComDesconto = Produtos.Where(
                produto => produto.Produto.Id == CupomDeDesconto.SetorDeDesconto ||
                produto.Produto.Subcategoria.Id == CupomDeDesconto.SetorDeDesconto ||
                produto.Produto.Subcategoria.Categoria.Id == CupomDeDesconto.SetorDeDesconto).ToList();

            foreach (var produto in _produtosComDesconto)
            {
                _subtotal += produto.Quantidade * produto.Produto.Valor;
            }

            if (CupomDeDesconto.TipoDeDesconto == nameof(TipoDeDesconto.Percentual))
            {
                _subtotal -= _subtotal * (CupomDeDesconto.ValorDoDesconto / 100);
            }

            else
                _subtotal -= CupomDeDesconto.ValorDoDesconto;

            var _produtosSemDesconto = Produtos.Where(
                produto => produto.Produto.Id != CupomDeDesconto.SetorDeDesconto &&
                produto.Produto.Subcategoria.Id != CupomDeDesconto.SetorDeDesconto && 
                produto.Produto.Subcategoria.Categoria.Id != CupomDeDesconto.SetorDeDesconto).ToList();

            foreach (var produto in _produtosSemDesconto)
            {
                _subtotal += produto.Quantidade * produto.Produto.Valor;
            }

            return _subtotal;
        }

        if (CupomDeDesconto.TipoDeDesconto == nameof(TipoDeDesconto.Percentual))
            return Subtotal - (Subtotal * (CupomDeDesconto.ValorDoDesconto / 100));

        return Subtotal - CupomDeDesconto.ValorDoDesconto;
    }
}

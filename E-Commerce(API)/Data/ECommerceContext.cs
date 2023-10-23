using E_Commerce_API_.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_API_.Data;

public class ECommerceContext : DbContext
{
	public ECommerceContext(DbContextOptions<ECommerceContext> options) : base(options)
	{

	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<ProdutoNoCarrinho>()
			.HasKey(produtoNoCarrinho => new { produtoNoCarrinho.CarrinhoDeComprasId, produtoNoCarrinho.ProdutoId });

		modelBuilder.Entity<ProdutoNoCarrinho>()
			.HasOne(produtoNoCarrinho => produtoNoCarrinho.CarrinhoDeCompras)
			.WithMany(carrinhoDeCompras => carrinhoDeCompras.Produtos)
			.HasForeignKey(produtoNoCarrinho => produtoNoCarrinho.CarrinhoDeComprasId);
	}

	public DbSet<Categoria> Categorias { get; set; }
	public DbSet<Subcategoria> Subcategorias { get; set; }
	public DbSet<Produto> Produtos { get; set; }
	public DbSet<CentroDeDistribuicao> CentrosDeDistribuicao { get; set; }
	public DbSet<CarrinhoDeCompras> CarrinhosDeCompras { get; set; }
	public DbSet<ProdutoNoCarrinho> ProdutosNoCarrinho { get; set; }
	public DbSet<CupomDeDesconto> CuponsDeDesconto { get; set; }
   
}

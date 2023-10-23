namespace E_Commerce_API_.Data.DTOs.ProdutoDto;

public class ReadProdutoDto
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public double Peso { get; set; }
    public double Altura { get; set; }
    public double Largura { get; set; }
    public double Comprimento { get; set; }
    public double Valor { get; set; }
    public int QuantidadeEmEstoque { get; set; }
    public bool Status { get; set; }
    public string Criacao { get; set; }
    public string Modificacao { get; set; }
    public string CentroDeDistribuicao { get; set; }
    public string Categoria { get; set; }
    public string Subcategoria { get; set; }
}

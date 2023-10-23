using System.ComponentModel.DataAnnotations;

namespace E_Commerce_API_.Models;

public class Produto
{
    [Key]
    [Required]
    public Guid Id { get; set; }
    [Required(ErrorMessage = "O Nome do produto é obrigatório.")]
    [MaxLength(128)]
    public string Nome { get; set; }
    [Required(ErrorMessage = "Insira uma descrição para o produto.")]
    [MaxLength(512)]
    public string Descricao { get; set; }
    [Required(ErrorMessage = "Insira um valor decimal para o peso do produto.")]
    public double Peso { get; set; }
    [Required(ErrorMessage = "Insira um valor decimal para a altura do produto.")]
    public double Altura { get; set; }
    [Required(ErrorMessage = "Insira um valor decimal para a largura do produto.")]
    public double Largura { get; set; }
    [Required(ErrorMessage = "Insira um valor decimal para o comprimento do produto.")]
    public double Comprimento { get; set; }
    [Required(ErrorMessage = "Insira um valor decimal para o valor do produto.")]
    public double Valor { get; set; }
    [Required(ErrorMessage = "Insira a quantidade de produtos no estoque para cadastro.")]
    public int QuantidadeEmEstoque { get; set; }
    public bool Status { get; set; }
    public DateTime DataEHoraCriacao { get; set; }
    public DateTime DataEHoraModificacao { get; set; }
    [Required(ErrorMessage = "Insira o ID da Subcategoria em que o produto se encontra.")]
    public Guid SubcategoriaId { get; set; }
    [Required(ErrorMessage = "Insira o ID do Centro de Distribuição em que o produto se encontra.")]
    public Guid CentroDeDistribuicaoId { get; set; }
    public virtual CentroDeDistribuicao CentroDeDistribuicao { get; set; }
    public virtual Subcategoria Subcategoria { get; set; }
}

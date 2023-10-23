using E_Commerce_API_.Attributes;
using System.ComponentModel.DataAnnotations;

namespace E_Commerce_API_.Models;

public class Subcategoria
{
    [Key]
    [Required]
    public Guid Id { get; set; }
    [Required(ErrorMessage = "O campo Nome é obrigatório")]
    [VerificacaoDoNome]
    public string Nome { get; set; }
    public bool Status { get; set; }
    public DateTime DataEHoraCriacao { get; set; }
    public DateTime DataEHoraModificacao { get; set; }
    [Required]
    public Guid CategoriaId { get; set; }
    public virtual Categoria Categoria { get; set; }
    public virtual ICollection<Produto> Produtos { get; set; }
}

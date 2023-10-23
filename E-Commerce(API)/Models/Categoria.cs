using E_Commerce_API_.Attributes;
using System.ComponentModel.DataAnnotations;

namespace E_Commerce_API_.Models;

public class Categoria
{
    [Key]
    [Required]
    public Guid Id { get; set; }
    [Required]
    [VerificacaoDoNome]
    public string Nome { get; set; }
    public bool Status { get; set; } = true;
    public DateTime DataEHoraCriacao { get; set; }
    public DateTime DataEHoraModificacao { get; set; }
    public virtual ICollection<Subcategoria> Subcategorias { get; set; }
}

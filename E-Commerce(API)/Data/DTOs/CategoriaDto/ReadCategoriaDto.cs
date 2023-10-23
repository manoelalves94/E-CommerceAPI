using E_Commerce_API_.Attributes;
using System.ComponentModel.DataAnnotations;

namespace E_Commerce_API_.Data.DTOs.CategoriaDto;

public class ReadCategoriaDto
{
    [Key]
    [Required]
    public Guid Id { get; set; }
    [Required]
    [VerificacaoDoNome]
    public string Nome { get; set; }
    public bool Status { get; set; }
    public string Criacao { get; set; }
    public string Modificacao { get; set; }
}

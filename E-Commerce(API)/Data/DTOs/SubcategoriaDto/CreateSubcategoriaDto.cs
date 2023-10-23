using E_Commerce_API_.Attributes;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace E_Commerce_API_.Data.DTOs.SubcategoriaDto;

public class CreateSubcategoriaDto
{
    TextInfo ti = CultureInfo.CurrentCulture.TextInfo;
    private string _nome;
    public Guid Id { get; set; } = Guid.NewGuid();
    [Obrigatorio]
    [VerificacaoDoNome]
    public string Nome { get { return _nome; } set { _nome = ti.ToTitleCase(value.ToLower()); } }
    public bool Status { get; set; } = true;
    public DateTime DataEHoraCriacao { get; set; } = DateTime.Now;
    public Guid CategoriaId { get; set; }
}

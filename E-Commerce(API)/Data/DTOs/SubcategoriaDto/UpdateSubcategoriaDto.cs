using E_Commerce_API_.Attributes;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace E_Commerce_API_.Data.DTOs.SubcategoriaDto;

public class UpdateSubcategoriaDto
{
    TextInfo ti = CultureInfo.CurrentCulture.TextInfo;
    private string _nome;
    [Required]
    [VerificacaoDoNome]
    public string Nome { get { return _nome; } set { _nome = ti.ToTitleCase(value.ToLower()); } }
    [Required(ErrorMessage = "O Status deve ser definido como false(inativo) ou true(ativo).")]
    public bool Status { get; set; }
    public DateTime DataEHoraModificacao { get; set; } = DateTime.Now;
}

using E_Commerce_API_.Attributes;
using System.Globalization;

namespace E_Commerce_API_.Data.DTOs.CategoriaDto;

public class UpdateCategoriaDto
{
    TextInfo ti = CultureInfo.CurrentCulture.TextInfo;
    private string _nome;
    public DateTime _dataEHoraModificacao = DateTime.Now;

    [Obrigatorio(ErrorMessage = "O campo Nome é obrigatório.")]
    [VerificacaoDoNome]
    public string Nome { get { return _nome; } set { _nome = ti.ToTitleCase(value.ToLower()); } }
    [Obrigatorio(ErrorMessage = "O campo Status é obrigatório.")]
    public bool Status { get; set; }
}

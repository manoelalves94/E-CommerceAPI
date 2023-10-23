using E_Commerce_API_.Attributes;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace E_Commerce_API_.Data.DTOs.CategoriaDto;

public class CreateCategoriaDto
{
    TextInfo ti = CultureInfo.CurrentCulture.TextInfo;
    private string _nome;
    public Guid _id = Guid.NewGuid();
    public bool _status = true;
    public DateTime _dataEHoraCriacao = DateTime.Now;

    [Obrigatorio(ErrorMessage = "O campo Nome é obrigatório.")]
    [VerificacaoDoNome]
    public string Nome { get { return _nome; } set { _nome = ti.ToTitleCase(value.ToLower()); } }
}

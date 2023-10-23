using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Usuarios_API_.Attributes;

namespace Usuarios_API_.Data.DTOs;

public class UpdateUserDto
{
    readonly TextInfo ti = CultureInfo.CurrentCulture.TextInfo;
    private string _nome;
    public DateTime _dataAtualizacao = DateTime.Now;

    [Obrigatorio]
    [TamanhoMaximo(250)]
    public string Nome { get { return _nome; } set { _nome = ti.ToTitleCase(value.ToLower()); } }
    [Obrigatorio]
    [VerificacaoDataNascimento]
    public DateTime DataNascimento { get; set; }
    [Obrigatorio]
    [DataType(DataType.EmailAddress, ErrorMessage = "Digite um e-mail num formato válido.")]
    public string Email { get; set; }
    [Obrigatorio]
    [TamanhoMinimo(8)]
    [TamanhoMaximo(8)]
    public string CEP { get; set; }
    [Obrigatorio]
    public uint Numero { get; set; }
    [Obrigatorio]
    public string Complemento { get; set; }
    [Obrigatorio]
    public bool Status { get; set; }
}

using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Usuarios_API_.Attributes;

namespace Usuarios_API_.Data.DTOs;

public class CreateUserDto
{
    readonly TextInfo ti = CultureInfo.CurrentCulture.TextInfo;
    private string _nome;

    [Obrigatorio]
    [TamanhoMaximo(250)]
    public string Nome { get { return _nome; } set { _nome = ti.ToTitleCase(value.ToLower()); } }

    [Obrigatorio]
    [VerificacaoDataNascimento]
    public DateTime DataNascimento { get; set; }

    [Obrigatorio]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    [Obrigatorio]
    [CPFValido]
    public string CPF { get; set; }

    [Obrigatorio]
    [DataType(DataType.Password)]
    public string Senha { get; set; }

    [Obrigatorio]
    [DataType(DataType.Password)]
    [Comparar("Senha", ErrorMessage = "As senhas não coincidem.")]
    public string ConfirmaSenha { get; set; }

    [Obrigatorio]
    public uint Numero { get; set; }

    [Obrigatorio]
    public string Complemento { get; set; }

    [Obrigatorio(ErrorMessage = "O campo CEP é obrigatório.")]
    [TamanhoMinimo(8)]
    [TamanhoMaximo(8)]
    public string CEP { get; set; }
}

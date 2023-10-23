using System.ComponentModel.DataAnnotations;

namespace Usuarios_API_.Data.Requests;

public class EfetuaResetSenhaRequest
{
    [Required]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }
    [Required]
    [DataType(DataType.Password)]
    public string Senha { get; set; }
    [Required]
    [DataType(DataType.Password)]
    [Compare("Senha")]
    public string ConfirmaSenha { get; set; }
    [Required]
    public string CodigoDeRedefinicao { get; set; }
}

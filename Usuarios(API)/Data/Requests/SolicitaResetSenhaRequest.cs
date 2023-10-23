using System.ComponentModel.DataAnnotations;

namespace Usuarios_API_.Data.Requests;

public class SolicitaResetSenhaRequest
{
    [Required]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }
}

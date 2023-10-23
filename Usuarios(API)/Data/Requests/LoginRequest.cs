using System.ComponentModel.DataAnnotations;

namespace Usuarios_API_.Data.Requests;

public class LoginRequest
{
    [Required]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }
    [Required]
    [DataType(DataType.Password)]
    public string Senha { get; set; }
}

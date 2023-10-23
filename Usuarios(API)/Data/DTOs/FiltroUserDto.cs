using System.ComponentModel.DataAnnotations;
using Usuarios_API_.Attributes;

namespace Usuarios_API_.Data.DTOs;

public class FiltroUserDto
{
    [TamanhoMinimo(3)]
    [TamanhoMaximo(250)]
    public string? Nome { get; set; }
    [TamanhoMinimo(11)]
    [TamanhoMaximo(11)]
    public string? CPF { get; set; }
    [DataType(DataType.EmailAddress, ErrorMessage = "Digite um e-mail num formato válido.")]
    public string? Email { get; set; }
    public Status Status { get; set; } = Status.Todos;
}

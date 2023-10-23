using Usuarios_API_.Attributes;

namespace Usuarios_API_.Data.Requests;

public class EditaStatusRequest
{
    [Obrigatorio(ErrorMessage = "O campo Id é obrigatório.")]
    public Guid Id { get; set; }
    [Obrigatorio(ErrorMessage = "O campo Status é obrigatório.")]
    public bool Status { get; set; }
}

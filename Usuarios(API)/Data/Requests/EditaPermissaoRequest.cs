using System.Globalization;
using Usuarios_API_.Attributes;

namespace Usuarios_API_.Data.Requests;

public class EditaPermissaoRequest
{
    readonly TextInfo ti = CultureInfo.CurrentCulture.TextInfo;
    private string _tipoDeUsuario;

    [Obrigatorio]
    public Guid Id { get; set; }
    [Obrigatorio]
    [RolesExistentes]
    public string TipoDeUsuario { get { return _tipoDeUsuario; } set { _tipoDeUsuario = ti.ToTitleCase(value.ToLower()); } }
}

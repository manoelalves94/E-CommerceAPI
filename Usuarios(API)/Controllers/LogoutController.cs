using Microsoft.AspNetCore.Mvc;
using Usuarios_API_.Interfaces;

namespace Usuarios_API_.Controllers;

[ApiController]
    [Route("[controller]")]

public class LogoutController : ControllerBase
{
    private ILogoutService _iLogoutService;

    public LogoutController(ILogoutService iLogoutService)
    {
        _iLogoutService = iLogoutService;
    }

    [HttpPost]
    public IActionResult DeslogaUsuario()
    {
        var result = _iLogoutService.DeslogaUsuario();

        return result.IsFailed ? Unauthorized(result.Errors) : Ok();
    }
}

using Microsoft.AspNetCore.Mvc;
using Usuarios_API_.Data.Requests;
using Usuarios_API_.Interfaces;

namespace Usuarios_API_.Controllers;

[ApiController]
[Route("[controller]")]

public class LoginController : ControllerBase
{
    private ILoginService _loginservice;

    public LoginController(ILoginService loginservice)
    {
        _loginservice = loginservice;
    }

    [HttpPost]
    public IActionResult LogaUsuario(LoginRequest request)
    {
        var login = _loginservice.LogaUsuario(request);

        return Ok(login);
    }
}

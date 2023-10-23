using FluentResults;
using Microsoft.AspNetCore.Identity;
using Usuarios_API_.Interfaces;
using Usuarios_API_.Models;

namespace Usuarios_API_.Services;

public class LogoutService : ILogoutService
{
    private SignInManager<CustomIdentityUser> _signInManager;

    public LogoutService(SignInManager<CustomIdentityUser> signInManager)
    {
        _signInManager = signInManager;
    }

    public Result DeslogaUsuario()
    {     
        var resultadoIdentity = _signInManager.SignOutAsync();

        return resultadoIdentity.IsCompletedSuccessfully ? Result.Ok() : Result.Fail("Operação não realizada.");
    }
}

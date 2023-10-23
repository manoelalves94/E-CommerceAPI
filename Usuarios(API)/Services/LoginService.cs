using Microsoft.AspNetCore.Identity;
using Usuarios_API_.Data.Requests;
using Usuarios_API_.Exceptions;
using Usuarios_API_.Interfaces;
using Usuarios_API_.Models;

namespace Usuarios_API_.Services;

public class LoginService : ILoginService
{
    private SignInManager<CustomIdentityUser> _signInManager;
    private ITokenService _tokenService;

    public LoginService(SignInManager<CustomIdentityUser> signInManager, ITokenService tokenService)
    {
        _signInManager = signInManager;
        _tokenService = tokenService;
    }

    public Token LogaUsuario(LoginRequest request)
    {
        var identityUser = _signInManager.UserManager.FindByEmailAsync(request.Email).Result;
        if (identityUser == null) throw new NotFoundException("E-mail não existe");
        if (identityUser.Status == false) throw new BadRequestException("Usuário está inativo no sistema e não pode logar.");
        
        var resultadoIdentity = _signInManager.PasswordSignInAsync(identityUser.UserName, request.Senha, false, false).Result;

        if (resultadoIdentity.Succeeded)
        {
            var token = _tokenService.CreateToken(identityUser, _signInManager.UserManager.GetRolesAsync(identityUser).Result.First());
            return token;
        }
        throw new BadRequestException("Login falhou: Senha/E-mail inválidos.");
    }
}

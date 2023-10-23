using Microsoft.AspNetCore.Identity;
using Usuarios_API_.Models;

namespace Usuarios_API_.Interfaces;

public interface ITokenService
{
    Token CreateToken(CustomIdentityUser usuario, string role);
}

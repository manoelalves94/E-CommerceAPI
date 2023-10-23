using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Usuarios_API_.Interfaces;
using Usuarios_API_.Models;

namespace Usuarios_API_.Services;

public class TokenService : ITokenService
{
    private IConfiguration _configuration;

    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public Token CreateToken(CustomIdentityUser usuario, string role)
    {
        var direitosUsuario = new Claim[]
        {
            new Claim("username", usuario.UserName),
            new Claim("id", usuario.Id.ToString()),
            new Claim(ClaimTypes.Role, role)
        };

        var chave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("hash")));

        var credenciais = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            claims: direitosUsuario,
            signingCredentials: credenciais,
            expires: DateTime.UtcNow.AddHours(24));

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

        return new Token(tokenString);
    }
}

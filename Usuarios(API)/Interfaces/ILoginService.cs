using FluentResults;
using Usuarios_API_.Data.Requests;
using Usuarios_API_.Models;

namespace Usuarios_API_.Interfaces;

public interface ILoginService
{
    Token LogaUsuario(LoginRequest request);
}

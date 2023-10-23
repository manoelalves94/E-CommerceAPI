using FluentResults;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Usuarios_API_.Data.DTOs;
using Usuarios_API_.Data.Requests;
using Usuarios_API_.Data.Responses;

namespace Usuarios_API_.Interfaces;

public interface IUserService
{
    Task<ReadUserDto> CadastraUser(CreateUserDto userDto, TipoUser role);
    void DeletaUser(Guid id);
    void EditaPermissaoUser(EditaPermissaoRequest request);
    void EditaStatusUser(EditaStatusRequest request);
    Task EditaUser(Guid id, UpdateUserDto clienteDto);
    List<ReadUserDto> ListaUsuarios(FiltroUserDto filtro);
    void ResetaSenha(EfetuaResetSenhaRequest request);
    CodigoDeRecuperacaoResponse SolicitaResetSenha(SolicitaResetSenhaRequest request);
    ReadUserDto UsuarioLogado(string value);
}

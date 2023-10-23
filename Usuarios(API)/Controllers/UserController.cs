using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Usuarios_API_.Data.DTOs;
using Usuarios_API_.Data.Requests;
using Usuarios_API_.Interfaces;

namespace Usuarios_API_.Controllers;

[ApiController]
[Route("[controller]")]

public class UserController : ControllerBase
{
    private IUserService _iClienteService;

    public UserController(IUserService iClienteService)
    {
        _iClienteService = iClienteService;
    }

    [HttpPost("/cliente")]
    public async Task<IActionResult> CadastraCliente([FromBody] CreateUserDto userDto)
    {
        var clienteDto = await _iClienteService.CadastraUser(userDto, TipoUser.Cliente);

        return CreatedAtAction(nameof(UsuarioLogado), clienteDto);
    }

    [HttpPost("/lojista")]
    [Authorize(Roles = "admin, lojista")]
    public async Task<IActionResult> CadastraLojista([FromBody] CreateUserDto userDto)
    {
        var lojistaDto = await _iClienteService.CadastraUser(userDto, TipoUser.Lojista);

        return CreatedAtAction(nameof(UsuarioLogado), lojistaDto);
    }

    [HttpPut("/perfil/{id}")]
    public async Task<IActionResult> EditaUser(Guid id, [FromBody] UpdateUserDto clienteDto)
    {
        await _iClienteService.EditaUser(id, clienteDto);

        return NoContent();
    }

    [HttpPost("/solicita-reset")]
    public IActionResult SolicitaResetSenha([FromBody] SolicitaResetSenhaRequest request)
    {
        var response = _iClienteService.SolicitaResetSenha(request);

        return Ok(response);
    }

    [HttpPost("/efetua-reset")]
    public IActionResult ResetaSenha([FromBody] EfetuaResetSenhaRequest request)
    {
        _iClienteService.ResetaSenha(request);

        return Ok("Senha redefinida com sucesso.");
    }

    [HttpGet("/pesquisa")]
    [Authorize(Roles = "admin")]
    public IActionResult ListaUsuarios([FromQuery] FiltroUserDto filtro)
    {
        var listaDto = _iClienteService.ListaUsuarios(filtro);

        return listaDto.Any() ? Ok(listaDto) : NotFound();
    }

    [HttpGet("/meu-perfil")]
    [Authorize]
    public IActionResult UsuarioLogado()
    {
        var userClaims = HttpContext.User.Claims.ToList();

        var userDto = _iClienteService.UsuarioLogado(userClaims[1].Value);

        return Ok(userDto);
    }

    [HttpDelete("{id}")]
    [Authorize]
    public IActionResult DeletaUser(Guid id)
    {
        var userClaims = HttpContext.User.Claims.ToList();

        if (userClaims[1].Value != id.ToString() && userClaims[2].Value != "admin")
            return Unauthorized("Não é possível excluir cadastro de outro usuário. É necessário logar com a conta a ser excluída.");

        _iClienteService.DeletaUser(id);

        return NoContent();
    }

    [HttpPut("/permissao")]
    [Authorize(Roles = "admin")]
    public IActionResult EditaPermissaoUser([FromBody] EditaPermissaoRequest request)
    {
        _iClienteService.EditaPermissaoUser(request);

        return NoContent();
    }

    [HttpPut("/status")]
    [Authorize(Roles = "admin")]
    public IActionResult EditaStatusUser([FromBody] EditaStatusRequest request)
    {
        _iClienteService.EditaStatusUser(request);

        return NoContent();
    }
}

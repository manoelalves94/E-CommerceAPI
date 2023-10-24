using E_Commerce_API_.Data.DTOs;
using E_Commerce_API_.Data.DTOs.CentroDeDistribuicaoDto;
using E_Commerce_API_.Data.DTOs.Error;
using E_Commerce_API_.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Data;

namespace E_Commerce_API_.Controllers;

[ApiController]
[Route("[controller]")]

public class CentroDeDistribuicaoController : ControllerBase
{
    private ICentroDeDistribuicaoService _iCentroDeDistribuicaoService;

    public CentroDeDistribuicaoController(ICentroDeDistribuicaoService iCentroDeDistribuicaoService)
    {
        _iCentroDeDistribuicaoService = iCentroDeDistribuicaoService;
    }

    [HttpPost]
    [Authorize(Roles = "admin, lojista")]
    public async Task<IActionResult> CadastrarCD([FromBody] CreateCentroDeDistribuicaoDto centroDeDistribuicaoDto)
    {
        var userClaims = HttpContext.User.Claims.ToList();

        Log.Information($"Foi requisitado um cadastro de centro de distribuição pelo usuário: '{userClaims[1].Value}'.");

        var cd = await _iCentroDeDistribuicaoService.AdicionarCD(centroDeDistribuicaoDto);

        var links = LinkDto.CreateLinks(this, cd.Id, "CentroDeDistribuicao");

        var response = new ResponseDto<ReadCentroDeDistribuicaoDto>(cd, links);

        return CreatedAtAction(nameof(PesquisarCDPorId), new { id = cd.Id }, response);
    }

    [HttpGet("{id}", Name = "GetCentroDeDistribuicao")]
    public IActionResult PesquisarCDPorId(Guid id)
    {
        Log.Information("Foi requisitado uma pesquisa de centro de distribuição por Id.");

        var cd = _iCentroDeDistribuicaoService.PesquisarCDPorId(id);

        var links = LinkDto.CreateLinks(this, id, "CentroDeDistribuicao");

        var response = new ResponseDto<ReadCentroDeDistribuicaoDto>(cd, links);

        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> PesquisarCD([FromBody] FiltroCentroDeDistribuicaoDto filtro)
    {
        Log.Information("Foi requisitado uma pesquisa de centros de distribuição.");

        var centros = await _iCentroDeDistribuicaoService.ListaFiltrada(filtro);

        return centros.Any() ? Ok(centros) : NotFound();
    }

    [HttpPut("{id}", Name = "UpdateCentroDeDistribuicao")]
    [Authorize(Roles = "admin, lojista")]
    public async Task<IActionResult> EditarCD(Guid id, [FromBody] UpdateCentroDeDistribuicaoDto centroDeDistribuicaoDto)
    {
        var userClaims = HttpContext.User.Claims.ToList();

        Log.Information($"Foi requisitado uma edição de centro de distribuição pelo usuário: '{userClaims[1].Value}'.");

        await _iCentroDeDistribuicaoService.EditarCD(id, centroDeDistribuicaoDto);

        return NoContent();
    }

    [HttpDelete("{id}", Name = "DeleteCentroDeDistribuicao")]
    [Authorize(Roles = "admin, lojista")]
    public IActionResult RemoverCD(Guid id)
    {
        var userClaims = HttpContext.User.Claims.ToList();

        Log.Information($"Foi requisitado uma remoção de centro de distribuição pelo usuário: '{userClaims[1].Value}'.");

        _iCentroDeDistribuicaoService.DeletarCD(id);

        return NoContent();
    }
}

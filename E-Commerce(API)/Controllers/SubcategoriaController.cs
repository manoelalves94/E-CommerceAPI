using Serilog;
using Microsoft.AspNetCore.Mvc;
using E_Commerce_API_.Interfaces;
using E_Commerce_API_.Data.DTOs.SubcategoriaDto;
using E_Commerce_API_.Data.DTOs.Error;
using E_Commerce_API_.Data.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace E_Commerce_API_.Controllers;

[ApiController]
[Route("[controller]")]

public class SubcategoriaController : ControllerBase
{
    private ICategoriaService _iCategoriaService;
    private ISubcategoriaService _iSubcategoriaService;

    public SubcategoriaController(ICategoriaService iCategoriaService, ISubcategoriaService iSubcategoriaService)
    {
        _iCategoriaService = iCategoriaService;
        _iSubcategoriaService = iSubcategoriaService;
    }

    [HttpPost]
    [Authorize(Roles = "admin, lojista")]
    public IActionResult CadastrarSubcategoria([FromBody] CreateSubcategoriaDto subcategoriaDto)
    {
        var userClaims = HttpContext.User.Claims.ToList();

        Log.Information($"Foi requisitado um cadastro de subcategoria pelo usuário: '{userClaims[1].Value}'.");

        var subcategoria = _iSubcategoriaService.AdicionarSubcategoria(subcategoriaDto);

        var links = LinkDto.CreateLinks(this, subcategoria.Id, "Subcategoria");

        var response = new ResponseDto<ReadSubcategoriaDto>(subcategoria, links);

        return CreatedAtAction(nameof(PesquisarSubcategoriaId), new { id = subcategoria.Id }, response);
    }

    [HttpGet("{id}", Name = "GetSubcategoria")]
    public IActionResult PesquisarSubcategoriaId(Guid id)
    {
        Log.Information("Foi requisitado uma pesquisa de subcategoria por Id.");

        var subcategoria = _iSubcategoriaService.PesquisarSubcategoriaId(id);

        var links = LinkDto.CreateLinks(this, id, "Subcategoria");

        var response = new ResponseDto<ReadSubcategoriaDto>(subcategoria, links);

        return Ok(response);
    }

    [HttpGet]
    public IActionResult PesquisarSubcategorias([FromQuery] FiltroSubcategoriaDto filtro)
    {
        Log.Information("Foi requisitado uma pesquisa de subcategorias.");

        var subcategorias = _iSubcategoriaService.ListaFiltrada(filtro);

        return subcategorias.Any() ? Ok(subcategorias) : NotFound();
    }

    [HttpPut("{id}", Name = "UpdateSubcategoria")]
    [Authorize(Roles = "admin, lojista")]
    public IActionResult EditarSubcategoria(Guid id, [FromBody] UpdateSubcategoriaDto subcategoriaDto)
    {
        var userClaims = HttpContext.User.Claims.ToList();

        Log.Information($"Foi requisitado uma edição de subcategoria pelo usuário: '{userClaims[1].Value}'.");

        _iSubcategoriaService.AtualizarSubcategoria(id, subcategoriaDto);

        return NoContent();
    }

    [HttpDelete("{id}", Name = "DeleteSubcategoria")]
    [Authorize(Roles = "admin, lojista")]
    public IActionResult RemoverSubcategoria(Guid id)
    {
        var userClaims = HttpContext.User.Claims.ToList();

        Log.Information($"Foi requisitado uma remoção de subcategoria pelo usuário: '{userClaims[1].Value}'.");

        _iSubcategoriaService.RemoverSubcategoria(id);

        return NoContent();
    }
}

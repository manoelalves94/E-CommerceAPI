using Serilog;
using Microsoft.AspNetCore.Mvc;
using E_Commerce_API_.Interfaces;
using E_Commerce_API_.Data.DTOs.CategoriaDto;
using E_Commerce_API_.Data.DTOs;
using E_Commerce_API_.Data.DTOs.Error;
using Microsoft.AspNetCore.Authorization;

namespace E_Commerce_API_.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoriaController : ControllerBase
{
    private ICategoriaService _iCategoriaService;

    public CategoriaController(ICategoriaService iCategoriaService)
    {
        _iCategoriaService = iCategoriaService;
    }

    [HttpPost]
    //[Authorize(Roles = "admin, lojista")]
    public IActionResult CadastrarCategoria([FromBody] CreateCategoriaDto categoriaDto)
    {
        //var userClaims = HttpContext.User.Claims.ToList();

        //Log.Information($"Foi requisitado um cadastro de categoria pelo usuário: '{userClaims[1].Value}'.");

        var categoria = _iCategoriaService.AdicionarCategoria(categoriaDto);

        var links = LinkDto.CreateLinks(this, categoria.Id, "Categoria");

        var response = new ResponseDto<ReadCategoriaDto>(categoria, links);

        return CreatedAtAction(nameof(PesquisarCategoriaId), new { id = categoria.Id }, response);
    }

    [HttpGet("{id}", Name = "GetCategoria")]
    public IActionResult PesquisarCategoriaId(Guid id)
    {
        Log.Information("Foi requisitado uma pesquisa de categoria por Id.");

        var categoria = _iCategoriaService.PesquisarCategoriaId(id);

        var links = LinkDto.CreateLinks(this, id, "Categoria");

        var response = new ResponseDto<ReadCategoriaDto>(categoria, links);

        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> PesquisarCategorias([FromQuery] FiltroCategoriaDto filtro)
    {
        Log.Information("Foi requisitado uma pesquisa de categorias.");

        var categorias = await _iCategoriaService.ListaFiltrada(filtro);

        return categorias.Any() ? Ok(categorias) : NotFound();
    }

    [HttpPut("{id}", Name = "UpdateCategoria")]
    //[Authorize(Roles = "admin, lojista")]
    public IActionResult EditarCategoria(Guid id, [FromBody] UpdateCategoriaDto categoriaDto)
    {
        //var userClaims = HttpContext.User.Claims.ToList();

        //Log.Information($"Foi requisitado uma edição de categoria pelo usuário: '{userClaims[1].Value}'.");

        _iCategoriaService.AtualizarCategoria(id, categoriaDto);

        return NoContent();
    }

    [HttpDelete("{id}", Name = "DeleteCategoria")]
    //[Authorize(Roles = "admin, lojista")]
    public IActionResult RemoverCategoria(Guid id)
    {
        //var userClaims = HttpContext.User.Claims.ToList();

        //Log.Information($"Foi requisitado uma remoção de categoria pelo usuário: '{userClaims[1].Value}'.");

        _iCategoriaService.RemoverCategoria(id);

        return NoContent();
    }
}
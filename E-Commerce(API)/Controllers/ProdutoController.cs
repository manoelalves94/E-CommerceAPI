using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using E_Commerce_API_.Data.DTOs.ProdutoDto;
using E_Commerce_API_.Interfaces;
using Serilog;
using E_Commerce_API_.Data.DTOs;
using E_Commerce_API_.Data.DTOs.Error;
using Microsoft.AspNetCore.Authorization;

namespace E_Commerce_API_.Controllers;

[ApiController]
[Route("[controller]")]

public class ProdutoController : ControllerBase
{
    private IMapper _mapper;
    private IProdutoService _iProdutoService;
    private ISubcategoriaService _iSubcategoriaService;
    public ProdutoController(IMapper mapper, ISubcategoriaService iSubcategoriaService, IProdutoService iProdutoService)
    {
        _mapper = mapper;
        _iSubcategoriaService = iSubcategoriaService;
        _iProdutoService = iProdutoService;
    }

    [HttpPost]
    //[Authorize(Roles = "admin, lojista")]
    public IActionResult CadastrarProduto([FromBody] CreateProdutoDto produtoDto)
    {
        //var userClaims = HttpContext.User.Claims.ToList();

        //Log.Information($"Foi requisitado um cadastro de produto pelo usuário: '{userClaims[1].Value}'.");

        var produto = _iProdutoService.AdicionarProduto(produtoDto);

        var links = LinkDto.CreateLinks(this, produto.Id, "Produto");

        var response = new ResponseDto<ReadProdutoDto>(produto, links);

        return CreatedAtAction(nameof(PesquisarProdutoId), new { id = produto.Id }, response);
    }

    [HttpGet("{id}", Name = "GetProduto")]
    public IActionResult PesquisarProdutoId(Guid id)
    {
        Log.Information("Foi requisitado uma pesquisa de produto por Id.");

        var produto = _iProdutoService.PesquisarProdutoId(id);

        var links = LinkDto.CreateLinks(this, id, "Produto");

        var response = new ResponseDto<ReadProdutoDto>(produto, links);

        return Ok(response);
    }

    [HttpGet]
    public IActionResult PesquisarProduto([FromQuery] FiltroProdutoDto filtroDto)
    {
        Log.Information("Foi requisitado uma pesquisa de produtos.");

        var produtos = _iProdutoService.ListaProdutos(filtroDto);

        return produtos.Any() ? Ok(produtos) : NotFound();
    }

    [HttpPut("{id}", Name = "UpdateProduto")]
    //[Authorize(Roles = "admin, lojista")]
    public IActionResult EditarProduto(Guid id, [FromBody] UpdateProdutoDto produtoDto)
    {
        //var userClaims = HttpContext.User.Claims.ToList();

        //Log.Information($"Foi requisitado uma edição de produto pelo usuário: '{userClaims[1].Value}'.");

        _iProdutoService.AtualizarProduto(id, produtoDto);

        return NoContent();
    }

    [HttpDelete("{id}", Name = "DeleteProduto")]
    //[Authorize(Roles = "admin, lojista")]
    public IActionResult RemoverProduto(Guid id)
    {
        //var userClaims = HttpContext.User.Claims.ToList();

        //Log.Information($"Foi requisitado uma remoção de produto pelo usuário: '{userClaims[1].Value}'.");

        _iProdutoService.RemoverProduto(id);

        return NoContent();
    }
}

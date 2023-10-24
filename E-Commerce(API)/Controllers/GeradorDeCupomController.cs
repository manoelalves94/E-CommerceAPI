using E_Commerce_API_.Data.DTOs.GeradorDeCupomDto;
using E_Commerce_API_.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_API_.Controllers;
[ApiController]
[Route("[controller]")]
public class GeradorDeCupomController : ControllerBase
{
    private readonly IGeradorDeCupomService _service;

	public GeradorDeCupomController(IGeradorDeCupomService service)
    {
        _service = service;
    }

    [HttpPost]
    [Authorize(Roles = "admin, lojista")]
    public IActionResult GerarCupomDeDesconto(CreateCupomDeDescontoDto create)
    {
        var readCupom = _service.GerarCupomDeDesconto(create);

        return Ok(readCupom);
    }

    [HttpPut("{cupom}/{status}")]
    [Authorize(Roles = "admin, lojista")]
    public IActionResult AtivarOuInativarCupom(string cupom, bool status)
    {
        _service.AtivarOuInativarCupom(cupom, status);

        return NoContent();
    }

    [HttpGet]
    public IActionResult ListarCupons()
    {
        var listaDeCupons = _service.ListarCupons();

        return Ok(listaDeCupons);
    }
}

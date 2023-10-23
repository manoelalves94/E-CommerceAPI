using E_Commerce_API_.Data.DTOs.CarrinhoDeComprasDto;
using E_Commerce_API_.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_API_.Controllers;

[ApiController]
[Route("[controller]")]

public class CarrinhoDeComprasController : ControllerBase
{
    private readonly ICarrinhoDeComprasService _service;

    public CarrinhoDeComprasController(ICarrinhoDeComprasService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> CriarCarrinho([FromBody] CreateCarrinhoDeComprasDto createDto)
    {
        var carrinho = await _service.CriarCarrinho(createDto);

        return CreatedAtAction(nameof(GetCarrinho), new { carrinhoDeComprasId = carrinho.Id }, carrinho);
    }

    [HttpPost("{carrinhoDeComprasId}/enderecoDeEntrega")]
    public async Task<IActionResult> AddEnderecoDeEntregaAoCarrinho(
        Guid carrinhoDeComprasId,
        AddEnderecoDeEntregaDto enderecoDto)
    {
        var carrinho = await _service.AddEnderecoDeEntregaAoCarrinho(carrinhoDeComprasId, enderecoDto);

        return Ok(carrinho);
    }

    [HttpPost("{carrinhoDeComprasId}")]
    public IActionResult AdicionarProdutosAoCarrinho(Guid carrinhoDeComprasId, [FromBody] AddProdutosAoCarrinhoDto addDto)
    {
        var carrinho = _service.AdicionarProdutosAoCarrinho(carrinhoDeComprasId, addDto);
        return Ok(carrinho);
    }

    [HttpPut("{carrinhoDeComprasId}/produto/{produtoId}")]
    public IActionResult AlterarQuantidadeDeProdutosNoCarrinho(
        Guid carrinhoDeComprasId,
        Guid produtoId,
        [FromBody] UpdateCarrinhoDeComprasDto updateDto)
    {
        var carrinho = _service.AlterarQuantidadeDeProdutosNoCarrinho(carrinhoDeComprasId, produtoId, updateDto);
        return Ok(carrinho);
    }

    [HttpGet("{carrinhoDeComprasId}")]
    public IActionResult GetCarrinho(Guid carrinhoDeComprasId)
    {
        var carrinho = _service.GetCarrinho(carrinhoDeComprasId);
        return Ok(carrinho);
    }

    [HttpDelete("{carrinhoDeComprasId}/produto/{produtoId}")]
    public IActionResult RemoverProdutoDoCarrinho(Guid carrinhoDeComprasId, Guid produtoId)
    {
        var carrinho = _service.RemoverProdutoDoCarrinho(carrinhoDeComprasId, produtoId);
        return Ok(carrinho);
    }

    [HttpDelete("{carrinhoDeComprasId}")]
    public IActionResult ExcluirCarrinho(Guid carrinhoDeComprasId)
    {
        _service.ExcluirCarrinho(carrinhoDeComprasId);
        return Ok();
    }

    [HttpPost("{carrinhoDeComprasId}/cupom/{cupom}")]
    public IActionResult InserirCupomDeDesconto(Guid carrinhoDeComprasId, string cupom)
    {
        var carrinho = _service.InserirCupomDeDesconto(carrinhoDeComprasId, cupom);

        return Ok(carrinho);
    }

    [HttpDelete("{carrinhoDeComprasId}/cupom")]
    public IActionResult RemoverCupomDeDesconto(Guid carrinhoDeComprasId)
    {
        var carrinho = _service.RemoverCupomDeDesconto(carrinhoDeComprasId);
        return Ok(carrinho);
    }
}

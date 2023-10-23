using E_Commerce_API_.Attributes;

namespace E_Commerce_API_.Data.DTOs.CarrinhoDeComprasDto;

public class CreateCarrinhoDeComprasDto
{
    [Obrigatorio]
    public Guid ProdutoId { get; set; }
    [Obrigatorio]
    public uint Quantidade { get; set; }
}

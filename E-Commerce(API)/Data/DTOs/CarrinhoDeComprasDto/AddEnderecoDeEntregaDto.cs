using E_Commerce_API_.Attributes;

namespace E_Commerce_API_.Data.DTOs.CarrinhoDeComprasDto;

public class AddEnderecoDeEntregaDto
{
    [Obrigatorio]
    [TamanhoMaximo(8)]
    [TamanhoMinimo(8)]
    public string CEP { get; set; }
    [Obrigatorio]
    public uint Numero { get; set; }
    [Obrigatorio]
    public string Complemento { get; set; }
}

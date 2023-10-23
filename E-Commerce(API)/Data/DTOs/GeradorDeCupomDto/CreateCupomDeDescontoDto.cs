using E_Commerce_API_.Attributes;

namespace E_Commerce_API_.Data.DTOs.GeradorDeCupomDto;

public class CreateCupomDeDescontoDto
{
    [Obrigatorio]
    public string Cupom { get; set; }
    [Obrigatorio]
    public TipoDeDesconto TipoDeDesconto { get; set; }
    [Obrigatorio]
    public double ValorDoDesconto { get; set; }
    [Obrigatorio]
    public TipoDeUso Uso { get; set; }
    public Guid SetorDeDesconto { get; set; } = Guid.Empty;
}

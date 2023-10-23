namespace E_Commerce_API_.Data.DTOs.GeradorDeCupomDto;

public class ReadCupomDeDescontoDto
{
    public string Cupom { get; set; }
    public string TipoDeDesconto { get; set; }
    public double ValorDoDesconto { get; set; }
    public string Uso { get; set; }
    public bool Status { get; set; }
    public Guid SetorDeDesconto { get; set; }

    public bool ShouldSerializeSetorDeDesconto()
    {
        return SetorDeDesconto != Guid.Empty;
    }
}

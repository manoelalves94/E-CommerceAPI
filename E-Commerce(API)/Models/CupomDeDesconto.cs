using E_Commerce_API_.Attributes;
using System.ComponentModel.DataAnnotations;

namespace E_Commerce_API_.Models;

public class CupomDeDesconto
{
    public CupomDeDesconto()
    {
        Id = Guid.NewGuid();
        Status = true;
    }
    [Key]
    public Guid Id { get; set; }
    [Obrigatorio]
    public string Cupom { get; set; }
    public string TipoDeDesconto { get; set; }
    public double ValorDoDesconto { get; set; }
    public string Uso { get; set; }
    public bool Status { get; set; }
    public Guid SetorDeDesconto { get; set; }
    public virtual IEnumerable<CarrinhoDeCompras> CarrinhosDeCompras { get; set; }
}

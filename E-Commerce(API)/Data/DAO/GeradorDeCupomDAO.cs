using E_Commerce_API_.Exceptions;
using E_Commerce_API_.Interfaces;
using E_Commerce_API_.Models;

namespace E_Commerce_API_.Data.DAO;

public class GeradorDeCupomDAO : IGeradorDeCupomDAO
{
    private readonly ECommerceContext _context;

    public GeradorDeCupomDAO(ECommerceContext context)
    {
        _context = context;
    }

    public CupomDeDesconto BuscarCupom(string cupom)
    {
        var c = _context.CuponsDeDesconto.FirstOrDefault(c => c.Cupom == cupom);
        if (c == null)
            throw new BadRequestException("O cupom informado não existe");

        return c;
    }

    public void GerarCupomDeDesconto(CupomDeDesconto cupom)
    {
        _context.CuponsDeDesconto.Add(cupom);
        _context.SaveChanges();
    }

    public CupomDeDesconto GetCupomDeDesconto(string cupom)
    {
        var cupomDeDesconto = _context.CuponsDeDesconto.FirstOrDefault(cupons => cupons.Cupom == cupom);

        if (cupomDeDesconto == null)
            throw new BadRequestException("O cupom informado é inválido.");

        return cupomDeDesconto;
    }

    public List<CupomDeDesconto> GetCuponsDeDesconto()
    {
        return _context.CuponsDeDesconto.ToList();
    }

    public void UpdateCupom(CupomDeDesconto c)
    {
        _context.CuponsDeDesconto.Update(c);
        _context.SaveChanges();
    }
}

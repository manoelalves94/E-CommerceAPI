using E_Commerce_API_.Models;

namespace E_Commerce_API_.Interfaces;

public interface IGeradorDeCupomDAO
{
    CupomDeDesconto BuscarCupom(string cupom);
    void GerarCupomDeDesconto(CupomDeDesconto cupom);
    CupomDeDesconto GetCupomDeDesconto(string cupom);
    List<CupomDeDesconto> GetCuponsDeDesconto();
    void UpdateCupom(CupomDeDesconto c);
}

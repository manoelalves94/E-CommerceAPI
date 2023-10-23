using E_Commerce_API_.Data.DTOs.GeradorDeCupomDto;
using E_Commerce_API_.Models;

namespace E_Commerce_API_.Interfaces;

public interface IGeradorDeCupomService
{
    void AtivarOuInativarCupom(string cupom, bool status);
    ReadCupomDeDescontoDto GerarCupomDeDesconto(CreateCupomDeDescontoDto create);
    CupomDeDesconto GetCupomDeDesconto(string cupom);
    List<ReadCupomDeDescontoDto> ListarCupons();
}

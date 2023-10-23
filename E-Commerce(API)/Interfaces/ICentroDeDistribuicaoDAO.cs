using E_Commerce_API_.Data.DTOs.CentroDeDistribuicaoDto;
using E_Commerce_API_.Models;

namespace E_Commerce_API_.Interfaces;

public interface ICentroDeDistribuicaoDAO
{
    void Add(CentroDeDistribuicao c);
    void Update(CentroDeDistribuicao c);
    void Delete(CentroDeDistribuicao c);
    Task<IEnumerable<CentroDeDistribuicao>> GetCentrosDeDistribuicao(FiltroCentroDeDistribuicaoDto filtro);
    List<CentroDeDistribuicao> GetCentros();
}

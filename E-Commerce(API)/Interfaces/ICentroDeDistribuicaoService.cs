using E_Commerce_API_.Data.DTOs;
using E_Commerce_API_.Data.DTOs.CentroDeDistribuicaoDto;
using E_Commerce_API_.Models;
using FluentResults;

namespace E_Commerce_API_.Interfaces;

public interface ICentroDeDistribuicaoService
{
    Task<ReadCentroDeDistribuicaoDto> AdicionarCD(CreateCentroDeDistribuicaoDto centroDeDistribuicaoDto);
    Task EditarCD(Guid id, UpdateCentroDeDistribuicaoDto centroDeDistribuicaoDto);
    CentroDeDistribuicao? BuscarCDPorId(Guid id);
    void DeletarCD(Guid id);
    Task<List<ReadCentroDeDistribuicaoDto>> ListaFiltrada(FiltroCentroDeDistribuicaoDto filtro);
    ReadCentroDeDistribuicaoDto PesquisarCDPorId(Guid id);
}

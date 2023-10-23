using Microsoft.AspNetCore.Mvc;
using E_Commerce_API_.Data.DTOs.CategoriaDto;

namespace E_Commerce_API_.Interfaces;

public interface ICategoriaService
{
    Task<List<ReadCategoriaDto>> ListaFiltrada(FiltroCategoriaDto filtro);
    ReadCategoriaDto AdicionarCategoria(CreateCategoriaDto categoriaDto);
    void AtualizarCategoria(Guid id, [FromBody] UpdateCategoriaDto categoriaDto);
    void RemoverCategoria(Guid id);
    ReadCategoriaDto PesquisarCategoriaId(Guid id);
}

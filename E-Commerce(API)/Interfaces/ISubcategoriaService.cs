using E_Commerce_API_.Models;
using E_Commerce_API_.Data.DTOs.CategoriaDto;
using E_Commerce_API_.Data.DTOs.SubcategoriaDto;

namespace E_Commerce_API_.Interfaces;

public interface ISubcategoriaService
{
    List<ReadSubcategoriaDto> ListaFiltrada(FiltroSubcategoriaDto filtro);
    ReadSubcategoriaDto AdicionarSubcategoria(CreateSubcategoriaDto subcategoriaDto);
    void AtualizarSubcategoria(Guid id, UpdateSubcategoriaDto subcategoriaDto);
    void RemoverSubcategoria(Guid id);
    Subcategoria? BuscarSubcategoriaPorId(Guid id);
    void AlterarStatusSubcategorias(UpdateCategoriaDto categoriaDto, Categoria categoria);
    ReadSubcategoriaDto PesquisarSubcategoriaId(Guid id);
}

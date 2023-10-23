using E_Commerce_API_.Data.DTOs.SubcategoriaDto;
using E_Commerce_API_.Models;

namespace E_Commerce_API_.Interfaces;

public interface ISubcategoriaDAO
{
    void Create(Subcategoria s);
    void Update(UpdateSubcategoriaDto sDto, Subcategoria s);
    void Delete(Subcategoria s);
    List<Subcategoria> GetSubcategorias();
    List<ReadSubcategoriaDto> GetListaSubcategorias(FiltroSubcategoriaDto filtro);
}

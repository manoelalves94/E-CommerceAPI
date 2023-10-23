using E_Commerce_API_.Data.DTOs.CategoriaDto;
using E_Commerce_API_.Models;

namespace E_Commerce_API_.Interfaces;

public interface ICategoriaDAO
{
    void Add(Categoria c);
    void Update(Categoria c);
    void Delete(Categoria c);
    List<Categoria> GetCategorias();
    Task<IEnumerable<Categoria>> GetListaCategorias(FiltroCategoriaDto filtro);
}

using E_Commerce_API_.Data.DTOs.CategoriaDto;
using E_Commerce_API_.Data.DTOs.ProdutoDto;
using E_Commerce_API_.Models;

namespace E_Commerce_API_.Interfaces;

public interface IProdutoDAO
{
    void Add(Produto p);
    void Update(UpdateProdutoDto pDto, Produto p);
    void Delete(Produto p);
    List<ReadProdutoDto> GetListaProdutos(FiltroProdutoDto filtro);
    List<Produto> GetProdutos();
}

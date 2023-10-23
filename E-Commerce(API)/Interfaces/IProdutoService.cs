using E_Commerce_API_.Data.DTOs.ProdutoDto;
using E_Commerce_API_.Models;
using FluentResults;

namespace E_Commerce_API_.Interfaces;

public interface IProdutoService
{
    List<ReadProdutoDto> ListaProdutos(FiltroProdutoDto filtroDto);
    Produto? BuscarProdutoPorId(Guid id);
    ReadProdutoDto AdicionarProduto(CreateProdutoDto produtoDto);
    void AtualizarProduto(Guid id, UpdateProdutoDto produtoDto);
    void RemoverProduto(Guid id);
    ReadProdutoDto PesquisarProdutoId(Guid id);
    List<Produto> BuscarProdutosPorId(List<Guid> produtoId);
}

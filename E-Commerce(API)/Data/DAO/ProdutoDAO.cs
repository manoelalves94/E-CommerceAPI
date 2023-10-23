using AutoMapper;
using E_Commerce_API_.Data.DTOs.ProdutoDto;
using E_Commerce_API_.Interfaces;
using E_Commerce_API_.Models;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace E_Commerce_API_.Data.DAO;

public class ProdutoDAO : IProdutoDAO
{
    private ECommerceContext _context;
    private IMapper _mapper;

    public ProdutoDAO(ECommerceContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public void Add(Produto p)
    {
        _context.Produtos.Add(p);
        _context.SaveChanges();
        Log.Information("Produto com Id '{@Id}' incluído no banco de dados.", p.Id);
    }

    public void Update(UpdateProdutoDto pDto, Produto p)
    {
        _mapper.Map(pDto, p);
        _context.Update(p);
        _context.SaveChanges();
        Log.Information("Produto atualizado no banco de dados.");
    }

    public void Delete(Produto p)
    {
        _context.Produtos.Remove(p);
        _context.SaveChanges();
        Log.Information("Produto removido do banco de dados.");
    }

    public List<Produto> GetProdutos()
    {
        return _context.Produtos.ToList();
    }

    public List<ReadProdutoDto> GetListaProdutos(FiltroProdutoDto filtro)
    {
        Log.Information("Criação da querie para pesquisa de produtos.");

        var querie = "SELECT ";

        if (filtro.CategoriaId != null)
        {
            var categorias = filtro.CategoriaId.Split(',');
            var listaCategorias = new List<Guid>();
            foreach (var categoria in categorias)
            {
                listaCategorias.Add(new Guid(categoria));
            }

            querie += $"PRODUTOS.ID, PRODUTOS.NOME, PRODUTOS.ALTURA, PRODUTOS.CENTRODEDISTRIBUICAOID, PRODUTOS.COMPRIMENTO,PRODUTOS.DESCRICAO, PRODUTOS.DATAEHORACRIACAO, PRODUTOS.DATAEHORAMODIFICACAO, PRODUTOS.LARGURA, PRODUTOS.PESO, PRODUTOS.QUANTIDADEEMESTOQUE, PRODUTOS.STATUS, PRODUTOS.SUBCATEGORIAID, PRODUTOS.VALOR FROM `E-COMMERCE(API)`.PRODUTOS INNER JOIN `E-COMMERCE(API)`.SUBCATEGORIAS ON PRODUTOS.SUBCATEGORIAID = SUBCATEGORIAS.ID WHERE ";

            querie += "(";
            for (int i = 0; i < listaCategorias.Count; i++)
            {

                if (i < listaCategorias.Count - 1)
                    querie += $"CATEGORIAID = '{listaCategorias[i]}' OR ";
                else
                    querie += $"CATEGORIAID = '{listaCategorias[i]}' ";
            }
            querie += ") &&";
        }
        else
            querie += "* FROM `E-COMMERCE(API)`.PRODUTOS WHERE ";

        if (filtro.SubcategoriaId != null)
        {
            var subcategorias = filtro.SubcategoriaId.Split(',');
            var listaSubcategorias = new List<Guid>();
            foreach (var subcategoria in subcategorias)
            {
                listaSubcategorias.Add(new Guid(subcategoria));
            }

            querie += "(";
            for (int i = 0; i < listaSubcategorias.Count; i++)
            {
                if (i != listaSubcategorias.Count - 1)
                    querie += $"PRODUTOS.SUBCATEGORIAID = '{listaSubcategorias[i]}' OR ";
                else
                    querie += $"PRODUTOS.SUBCATEGORIAID = '{listaSubcategorias[i]}' ";
            }
            querie += ") &&";
        }

        if (filtro.CentroDeDistribuicaoId != null)
        {
            var centros = filtro.CentroDeDistribuicaoId.Split(",");
            var listaCentros = new List<Guid>();
            foreach (var centro in centros)
            {
                listaCentros.Add(new Guid(centro));
            }

            querie += "(";
            for (int i =0; i < listaCentros.Count; i++)
            {
                if (i != listaCentros.Count - 1)
                    querie += $"PRODUTOS.CENTRODEDISTRIBUICAOID = '{listaCentros[i]}' OR ";
                else
                    querie += $"PRODUTOS.CENTRODEDISTRIBUICAOID = '{listaCentros[i]}' ";
            }
            querie += ") &&";
        }

        if (filtro.Nome != null)
        {
            querie += $"LOCATE('{filtro.Nome}', PRODUTOS.NOME) && ";
        }

        if (filtro.Peso != null)
        {
            string p = filtro.Peso.ToString().Replace(',', '.');
            querie += $"PESO = {p} && ";
        }

        if (filtro.Altura != null)
        {
            string a = filtro.Altura.ToString().Replace(',', '.');
            querie += $"ALTURA = {a} && ";
        }

        if (filtro.Largura != null)
        {
            string l = filtro.Largura.ToString().Replace(',', '.');
            querie += $"LARGURA = {l} && ";
        }

        if (filtro.Comprimento != null)
        {
            string c = filtro.Comprimento.ToString().Replace(',', '.');
            querie += $"COMPRIMENTO = {c} && ";
        }

        if (filtro.Valor != null)
        {
            string v = filtro.Valor.ToString().Replace(',', '.');
            querie += $"VALOR = {v} && ";
        }

        if (filtro.Estoque != null)
        {
            querie += $"QUANTIDADEEMESTOQUE = {filtro.Estoque} && ";
        }

        switch (filtro.Status)
        {
            case Status.Inativo:
                querie += "PRODUTOS.STATUS = FALSE ";
                break;

            case Status.Ativo:
                querie += "PRODUTOS.STATUS = TRUE ";
                break;

            case Status.Todos:
                querie += "1=1 ";
                break;
        }

        if (filtro.Ordem == Ordem.Crescente)
            querie += $"ORDER BY PRODUTOS.NOME ";
        else if (filtro.Ordem == Ordem.Decrescente)
            querie += $"ORDER BY PRODUTOS.NOME DESC ";

        querie += $"LIMIT {(filtro.Pagina - 1) * filtro.Registros}, {filtro.Registros}";

        var produtos = _context.Produtos.FromSqlRaw(querie).ToList();

        var produtosDto = _mapper.Map<List<ReadProdutoDto>>(produtos);

        Log.Information($"Foram encontrados {produtosDto.Count} produtos a partir da querie: {querie}");

        return produtosDto;
    }

}

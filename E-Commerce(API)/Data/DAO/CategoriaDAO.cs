using Dapper;
using Serilog;
using MySql.Data.MySqlClient;
using E_Commerce_API_.Models;
using E_Commerce_API_.Interfaces;
using E_Commerce_API_.Data.DTOs.CategoriaDto;

namespace E_Commerce_API_.Data.DAO;

public class CategoriaDAO : ICategoriaDAO
{
    public ECommerceContext _context;
    private readonly string? connectionString;

    public CategoriaDAO(ECommerceContext context)
    {
        _context = context;
    }

    public CategoriaDAO(ECommerceContext context, IConfiguration configuration)
    {
        _context = context;
        connectionString = configuration.GetConnectionString("ECommerceConnection");
    }

    public void Add(Categoria c)
    {
        _context.Categorias.Add(c);
        _context.SaveChanges();
        Log.Information("Categoria com Id {@Id} incluída no banco de dados.", c.Id);
    }

    public void Update(Categoria c)
    {
        _context.Categorias.Update(c);
        _context.SaveChanges();
        Log.Information("Categoria atualizada no banco de dados.");
    }

    public void Delete(Categoria c)
    {
        _context.Categorias.Remove(c);
        _context.SaveChanges();
        Log.Information("Categoria removida do banco de dados.");
    }

    public List<Categoria> GetCategorias()
    {
        return _context.Categorias.ToList();
    }

    public async Task<IEnumerable<Categoria>> GetListaCategorias(FiltroCategoriaDto filtro)
    {
        Log.Information("Criação da querie para pesquisa de categorias.");

        string querie = "SELECT * FROM `E-COMMERCE(API)`.CATEGORIAS WHERE ";

        if (filtro.Nome != null)
        {
            querie += $"LOCATE ('{filtro.Nome}', NOME) AND ";
        }
        
        switch (filtro.Status)
        {
            case Status.Inativo:
                querie += $"STATUS = FALSE ";
                break;

            case Status.Ativo:
                querie += $"STATUS = TRUE ";
                break;

            case Status.Todos:
                querie += "1=1 ";
                break;
        }

        if (filtro.Ordem == Ordem.Crescente)
            querie += "ORDER BY NOME ";
        else if (filtro.Ordem == Ordem.Decrescente)
            querie += "ORDER BY NOME DESC ";

        querie += $"LIMIT {(filtro.Pagina - 1) * filtro.Registros}, {filtro.Registros}";

        IEnumerable<Categoria> categorias;
        using (var conection = new MySqlConnection(connectionString))
        {
            categorias = await conection.QueryAsync<Categoria>(querie);
        }

        Log.Information($"Foram encontrados {@categorias.Count()} categorias.");

        return categorias;
    }
}
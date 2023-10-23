using Dapper;
using E_Commerce_API_.Data.DTOs.CentroDeDistribuicaoDto;
using E_Commerce_API_.Interfaces;
using E_Commerce_API_.Models;
using MySql.Data.MySqlClient;
using Serilog;

namespace E_Commerce_API_.Data.DAO;

public class CentroDeDistribuicaoDAO : ICentroDeDistribuicaoDAO
{
    ECommerceContext _context;
    private readonly string? connectionSring;

    public CentroDeDistribuicaoDAO(ECommerceContext context)
    {
        _context = context;
    }

    public CentroDeDistribuicaoDAO(ECommerceContext context, IConfiguration configuration)
    {
        _context = context;
        connectionSring = configuration.GetConnectionString("ECommerceConnection");
    }

    public void Add(CentroDeDistribuicao c)
    {
        _context.CentrosDeDistribuicao.Add(c);
        _context.SaveChanges();
        Log.Information("Centro de distribuição com Id '{@Id}' incluído no banco de dados.", c.Id);
    }

    public void Update(CentroDeDistribuicao c)
    {
        _context.CentrosDeDistribuicao.Update(c);
        _context.SaveChanges();
        Log.Information("Centro de distribuição atualizado no banco de dados.");
    }

    public void Delete(CentroDeDistribuicao c)
    {
        _context.CentrosDeDistribuicao.Remove(c);
        _context.SaveChanges();
        Log.Information("Centro de distribuição removido do banco de dados.");
    }
    public List<CentroDeDistribuicao> GetCentros()
    {
        return _context.CentrosDeDistribuicao.ToList();
    }

    public async Task<IEnumerable<CentroDeDistribuicao>> GetCentrosDeDistribuicao(FiltroCentroDeDistribuicaoDto filtro)
    {
        Log.Information("Criação da querie para pesquisa de centros de distribuição.");

        string querie = "SELECT * FROM `E-COMMERCE(API)`.CENTROSDEDISTRIBUICAO WHERE ";

        if (filtro.Nome != null)
        {
            querie += $"LOCATE ('{filtro.Nome}', NOME) AND ";
        }

        if (filtro.Logradouro != null)
        {
            querie += $"LOCATE ('{filtro.Logradouro}', LOGRADOURO) AND ";
        }

        if (filtro.Bairro != null)
        {
            querie += $"LOCATE ('{filtro.Bairro}', BAIRRO) AND ";
        }

        if (filtro.Cidade != null)
        {
            querie += $"LOCATE ('{filtro.Cidade}', CIDADE) AND ";
        }

        if (filtro.UF != null)
        {
            querie += $"UF = '{filtro.UF}' AND ";
        }

        if (filtro.CEP != null)
        {
            querie += $"CEP = {filtro.CEP} AND ";
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

        IEnumerable<CentroDeDistribuicao> centros;
        using (var conection = new MySqlConnection(connectionSring))
        {
            centros = await conection.QueryAsync<CentroDeDistribuicao>(querie);
        }

        Log.Information($"Foram encontrados {centros.Count()} centros de distribuição a partir da querie: {querie}");
        return centros;
    }
}

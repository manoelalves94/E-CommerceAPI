using E_Commerce_API_.Data;
using E_Commerce_API_.Models;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_API_.Testes;

public class DBInMemory
{
    private ECommerceContext _context;
    public ECommerceContext GetContext() => _context;

    public DBInMemory()
    {
        var connection = new SqliteConnection("DataSource=:memory:");
        connection.Open();

        var options = new DbContextOptionsBuilder<ECommerceContext>()
            .UseSqlite(connection)
            .Options;

        _context = new ECommerceContext(options);
        InserirDados();
    }

    private void InserirDados()
    {
        if (_context.Database.EnsureCreated())
        {
            _context.Categorias.Add(
                new Categoria()
                {
                    Id = Guid.Parse("3e987cc6-3acf-4814-8c70-b27df656b001"),
                    Nome = "Comida",
                    Status = true,
                    DataEHoraCriacao = DateTime.Now,
                    DataEHoraModificacao = default
                });

            _context.Categorias.Add(
                new Categoria()
                {
                    Id = Guid.Parse("3e987cc6-3acf-4814-8c70-b27df656b002"),
                    Nome = "Bebida",
                    Status = true,
                    DataEHoraCriacao = DateTime.Now,
                    DataEHoraModificacao = default
                });

            _context.Categorias.Add(
                new Categoria()
                {
                    Id = Guid.Parse("3e987cc6-3acf-4814-8c70-b27df656b003"),
                    Nome = "Higiene",
                    Status = true,
                    DataEHoraCriacao = DateTime.Now,
                    DataEHoraModificacao = default
                });

            _context.Categorias.Add(
                new Categoria()
                {
                    Id = Guid.Parse("3e987cc6-3acf-4814-8c70-b27df656b004"),
                    Nome = "Eletrônicos",
                    Status = false,
                    DataEHoraCriacao = DateTime.Now,
                    DataEHoraModificacao = default
                });

            _context.Subcategorias.Add(
                new Subcategoria()
                {
                    Id = Guid.Parse("3e987cc6-3acf-4814-8c70-b27df656b005"),
                    Nome = "Carne",
                    Status = true,
                    DataEHoraCriacao = DateTime.Now,
                    DataEHoraModificacao = default,
                    CategoriaId = Guid.Parse("3e987cc6-3acf-4814-8c70-b27df656b001"),
                });

            _context.Subcategorias.Add(
                new Subcategoria()
                {
                    Id = Guid.Parse("3e987cc6-3acf-4814-8c70-b27df656b006"),
                    Nome = "Suco",
                    Status = true,
                    DataEHoraCriacao = DateTime.Now,
                    DataEHoraModificacao = default,
                    CategoriaId = Guid.Parse("3e987cc6-3acf-4814-8c70-b27df656b002"),
                });

            _context.Subcategorias.Add(
                new Subcategoria()
                {
                    Id = Guid.Parse("3e987cc6-3acf-4814-8c70-b27df656b007"),
                    Nome = "Refrigerante",
                    Status = true,
                    DataEHoraCriacao = DateTime.Now,
                    DataEHoraModificacao = default,
                    CategoriaId = Guid.Parse("3e987cc6-3acf-4814-8c70-b27df656b002"),
                });

            _context.SaveChanges();
        }

    }
}

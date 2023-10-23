using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Usuarios_API_.Models;

namespace Usuarios_API_.Data;

public class UserDbContext : IdentityDbContext<CustomIdentityUser, IdentityRole<Guid>, Guid>
{
    private IConfiguration _configuration;

    public UserDbContext(DbContextOptions<UserDbContext> opt, IConfiguration configuration) : base(opt)
    {
        _configuration = configuration;
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        var id = Guid.NewGuid();

        var admin = new CustomIdentityUser
        {
            UserName = "admin",
            NormalizedUserName = "ADMIN",
            Email = "admin@admin.com",
            NormalizedEmail = "ADMIN@ADMIN.COM",
            SecurityStamp = Guid.NewGuid().ToString(),
            Id = id,
            Logradouro = "Rua Cambuci",
            Numero = 49,
            Complemento = "Casa",
            Bairro = "Jardim Odete",
            Cidade = "Itaquaquecetuba",
            UF = "SP",
            CEP = "08598372",
            Status = true,
            CPF = "38479412917",
            TipoDeUsuario = "Admin"
        };

        var hasher = new PasswordHasher<CustomIdentityUser>();

        admin.PasswordHash = hasher.HashPassword(admin, _configuration.GetValue<string>("admininfo:password"));

        builder.Entity<CustomIdentityUser>().HasData(admin);

        builder.Entity<IdentityRole<Guid>>().HasData(
            new IdentityRole<Guid> { Id = id, Name = "admin", NormalizedName = "ADMIN" });

        builder.Entity<IdentityRole<Guid>>().HasData(
            new IdentityRole<Guid> { Id = Guid.NewGuid(), Name = "cliente", NormalizedName = "CLIENTE" });

        builder.Entity<IdentityRole<Guid>>().HasData(
            new IdentityRole<Guid> { Id = Guid.NewGuid(), Name = "lojista", NormalizedName = "LOJISTA" });

        builder.Entity<IdentityUserRole<Guid>>().HasData(
            new IdentityUserRole<Guid> { RoleId = id, UserId = id });
    }
}

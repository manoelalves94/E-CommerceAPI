using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Usuarios_API_.Migrations
{
    /// <inheritdoc />
    public partial class IncluindoCPF : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("1c4e1165-2825-4c0a-81a5-a46d9d93c110"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4e3781ab-a0f8-4953-9a80-e2794dc937de"));

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("34cc5f53-1484-4be6-aa2e-692990dfc136"), new Guid("34cc5f53-1484-4be6-aa2e-692990dfc136") });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("34cc5f53-1484-4be6-aa2e-692990dfc136"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("34cc5f53-1484-4be6-aa2e-692990dfc136"));

            migrationBuilder.AddColumn<string>(
                name: "CPF",
                table: "AspNetUsers",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("4d53081b-2b85-4291-9801-f61a82c6b2ce"), null, "cliente", "CLIENTE" },
                    { new Guid("6dc49af3-03f9-4956-909a-ff6498f2da03"), null, "admin", "ADMIN" },
                    { new Guid("93904639-bdfc-466e-884b-3b5656986b28"), null, "lojista", "LOJISTA" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Bairro", "CEP", "CPF", "Cidade", "Complemento", "ConcurrencyStamp", "DataEHoraCriacao", "DataEHoraModificacao", "DataNascimento", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Logradouro", "NormalizedEmail", "NormalizedUserName", "Numero", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Status", "TipoDeUsuario", "TwoFactorEnabled", "UF", "UserName" },
                values: new object[] { new Guid("6dc49af3-03f9-4956-909a-ff6498f2da03"), 0, "Jardim Odete", "08598372", "11111111111", "Itaquaquecetuba", "Casa", "73e17c74-3174-4d32-9753-51d6c3637e6f", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@admin.com", false, false, null, "Rua Cambuci", "ADMIN@ADMIN.COM", "ADMIN", 49u, "AQAAAAIAAYagAAAAEE2ytNn/prx/XWQeVxNUAtBH452MCIME/CRaoS4BBu+OzRmE2Q2tVXeR5DQ/qOKxiQ==", null, false, "12f01b6a-8c96-4632-a6c8-56648931f67a", true, "Admin", false, "SP", "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("6dc49af3-03f9-4956-909a-ff6498f2da03"), new Guid("6dc49af3-03f9-4956-909a-ff6498f2da03") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4d53081b-2b85-4291-9801-f61a82c6b2ce"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("93904639-bdfc-466e-884b-3b5656986b28"));

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("6dc49af3-03f9-4956-909a-ff6498f2da03"), new Guid("6dc49af3-03f9-4956-909a-ff6498f2da03") });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("6dc49af3-03f9-4956-909a-ff6498f2da03"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("6dc49af3-03f9-4956-909a-ff6498f2da03"));

            migrationBuilder.DropColumn(
                name: "CPF",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("1c4e1165-2825-4c0a-81a5-a46d9d93c110"), null, "lojista", "LOJISTA" },
                    { new Guid("34cc5f53-1484-4be6-aa2e-692990dfc136"), null, "admin", "ADMIN" },
                    { new Guid("4e3781ab-a0f8-4953-9a80-e2794dc937de"), null, "cliente", "CLIENTE" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Bairro", "CEP", "Cidade", "Complemento", "ConcurrencyStamp", "DataEHoraCriacao", "DataEHoraModificacao", "DataNascimento", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Logradouro", "NormalizedEmail", "NormalizedUserName", "Numero", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Status", "TipoDeUsuario", "TwoFactorEnabled", "UF", "UserName" },
                values: new object[] { new Guid("34cc5f53-1484-4be6-aa2e-692990dfc136"), 0, "Jardim Odete", "08598372", "Itaquaquecetuba", "Casa", "de22abbf-61c2-43d1-a9a8-95ccef29ee5e", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@admin.com", false, false, null, "Rua Cambuci", "ADMIN@ADMIN.COM", "ADMIN", 49u, "AQAAAAIAAYagAAAAEMnlX8RM3KRDZ0t/flgI9TiKDhyV0rbkBd2kv7n3/P7GsBjEOVtpXmL1zBpNAIxYUA==", null, false, "3d679875-54c1-4e8b-9b44-635b15591bcf", true, "Admin", false, "SP", "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("34cc5f53-1484-4be6-aa2e-692990dfc136"), new Guid("34cc5f53-1484-4be6-aa2e-692990dfc136") });
        }
    }
}

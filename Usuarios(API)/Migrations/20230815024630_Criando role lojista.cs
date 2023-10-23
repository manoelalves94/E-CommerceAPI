using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Usuarios_API_.Migrations
{
    /// <inheritdoc />
    public partial class Criandorolelojista : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("3b6edc54-770e-4999-a494-beb502ba56cb"));

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("331de21f-4c32-45c6-946e-e717770c9522"), new Guid("331de21f-4c32-45c6-946e-e717770c9522") });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("331de21f-4c32-45c6-946e-e717770c9522"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("331de21f-4c32-45c6-946e-e717770c9522"));

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("331de21f-4c32-45c6-946e-e717770c9522"), null, "admin", "ADMIN" },
                    { new Guid("3b6edc54-770e-4999-a494-beb502ba56cb"), null, "cliente", "CLIENTE" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Bairro", "CEP", "Cidade", "Complemento", "ConcurrencyStamp", "DataEHoraCriacao", "DataEHoraModificacao", "DataNascimento", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Logradouro", "NormalizedEmail", "NormalizedUserName", "Numero", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Status", "TipoDeUsuario", "TwoFactorEnabled", "UF", "UserName" },
                values: new object[] { new Guid("331de21f-4c32-45c6-946e-e717770c9522"), 0, "Jardim Odete", "08598372", "Itaquaquecetuba", "Casa", "32f292b5-542c-43fc-8a70-b3467efbbc59", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@admin.com", false, false, null, "Rua Cambuci", "ADMIN@ADMIN.COM", "ADMIN", 49u, "AQAAAAIAAYagAAAAENRDwFhSoOa5XJkbJd/x8/kKfed83vpEVW8zdE7Uqw49ClfjBBqDpUizl4t6SfX8MA==", null, false, "b0528cdb-e5f5-4e45-8aa6-96c57078ece6", true, "Admin", false, "SP", "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("331de21f-4c32-45c6-946e-e717770c9522"), new Guid("331de21f-4c32-45c6-946e-e717770c9522") });
        }
    }
}

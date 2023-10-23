using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Usuarios_API_.Migrations
{
    /// <inheritdoc />
    public partial class Tipodeusuário : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("0c548af6-41c5-4bf3-b2a4-2bbf9d826432"));

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("ad661f38-c19e-4577-826f-ee3e47b5c4fa"), new Guid("ad661f38-c19e-4577-826f-ee3e47b5c4fa") });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("ad661f38-c19e-4577-826f-ee3e47b5c4fa"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("ad661f38-c19e-4577-826f-ee3e47b5c4fa"));

            migrationBuilder.AddColumn<string>(
                name: "TipoDeUsuario",
                table: "AspNetUsers",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "TipoDeUsuario",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("0c548af6-41c5-4bf3-b2a4-2bbf9d826432"), null, "cliente", "CLIENTE" },
                    { new Guid("ad661f38-c19e-4577-826f-ee3e47b5c4fa"), null, "admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Bairro", "CEP", "Cidade", "Complemento", "ConcurrencyStamp", "DataEHoraCriacao", "DataEHoraModificacao", "DataNascimento", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Logradouro", "NormalizedEmail", "NormalizedUserName", "Numero", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Status", "TwoFactorEnabled", "UF", "UserName" },
                values: new object[] { new Guid("ad661f38-c19e-4577-826f-ee3e47b5c4fa"), 0, "Jardim Odete", "08598372", "Itaquaquecetuba", "Casa", "fa0a7f9e-cf45-498b-97e8-5bacb64826fb", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@admin.com", false, false, null, "Rua Cambuci", "ADMIN@ADMIN.COM", "ADMIN", 49u, "AQAAAAIAAYagAAAAEMJFodLLq6uvfkLJPieNgxsRLvwXdxHL8raGK/xlQkFB+H46U+H6n48W2B9v/vUHeQ==", null, false, "06a7e07a-2252-4d26-af74-0bb6d7fff837", false, false, "SP", "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("ad661f38-c19e-4577-826f-ee3e47b5c4fa"), new Guid("ad661f38-c19e-4577-826f-ee3e47b5c4fa") });
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Usuarios_API_.Migrations
{
    /// <inheritdoc />
    public partial class endereço : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("ad22515c-0759-4765-92b1-540a539ac72e"));

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("a9c3a9f3-36fd-4fe1-a223-677ad5fba9d6"), new Guid("a9c3a9f3-36fd-4fe1-a223-677ad5fba9d6") });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a9c3a9f3-36fd-4fe1-a223-677ad5fba9d6"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("a9c3a9f3-36fd-4fe1-a223-677ad5fba9d6"));

            migrationBuilder.AddColumn<string>(
                name: "Bairro",
                table: "AspNetUsers",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "CEP",
                table: "AspNetUsers",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Cidade",
                table: "AspNetUsers",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Complemento",
                table: "AspNetUsers",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataEHoraCriacao",
                table: "AspNetUsers",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DataEHoraModificacao",
                table: "AspNetUsers",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DataNascimento",
                table: "AspNetUsers",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Logradouro",
                table: "AspNetUsers",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<uint>(
                name: "Numero",
                table: "AspNetUsers",
                type: "int unsigned",
                nullable: false,
                defaultValue: 0u);

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "AspNetUsers",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UF",
                table: "AspNetUsers",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "Bairro",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CEP",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Cidade",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Complemento",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DataEHoraCriacao",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DataEHoraModificacao",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DataNascimento",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Logradouro",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Numero",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UF",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("a9c3a9f3-36fd-4fe1-a223-677ad5fba9d6"), null, "admin", "ADMIN" },
                    { new Guid("ad22515c-0759-4765-92b1-540a539ac72e"), null, "cliente", "CLIENTE" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("a9c3a9f3-36fd-4fe1-a223-677ad5fba9d6"), 0, "df26d41e-b64d-4128-85e0-1f3e96b7b076", "admin@admin.com", false, false, null, "ADMIN@ADMIN.COM", "ADMIN", "AQAAAAIAAYagAAAAEEOmRTEQk+FmZZPMdT/NdirE2h+c+yelG5OjpCKWauo4wX0Y5r0u5zjhbgTtSIwXag==", null, false, "61683353-a429-4e99-ad23-8798e42c071e", false, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("a9c3a9f3-36fd-4fe1-a223-677ad5fba9d6"), new Guid("a9c3a9f3-36fd-4fe1-a223-677ad5fba9d6") });
        }
    }
}

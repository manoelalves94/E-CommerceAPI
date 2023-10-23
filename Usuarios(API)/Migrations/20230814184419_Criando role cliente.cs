using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Usuarios_API_.Migrations
{
    /// <inheritdoc />
    public partial class Criandorolecliente : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("a6acdee6-20de-4c06-ae87-b87924e54431"), new Guid("a6acdee6-20de-4c06-ae87-b87924e54431") });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a6acdee6-20de-4c06-ae87-b87924e54431"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("a6acdee6-20de-4c06-ae87-b87924e54431"));

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("a6acdee6-20de-4c06-ae87-b87924e54431"), null, "admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("a6acdee6-20de-4c06-ae87-b87924e54431"), 0, "8c3182b4-b49f-45d7-a96b-01b10f2d1f88", "admin@admin.com", false, false, null, "ADMIN@ADMIN.COM", "ADMIN", "AQAAAAIAAYagAAAAEMDcYg6zothHbbz3dbLMA7AWfHiGGWlQ7dtM2gmVR0oXpLAednPdIRJNG13gxAjc9A==", null, false, "1b4980e6-4e99-48d3-a27b-c16a694fa243", false, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("a6acdee6-20de-4c06-ae87-b87924e54431"), new Guid("a6acdee6-20de-4c06-ae87-b87924e54431") });
        }
    }
}

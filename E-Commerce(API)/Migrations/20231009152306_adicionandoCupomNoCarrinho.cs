using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Commerce_API_.Migrations
{
    /// <inheritdoc />
    public partial class adicionandoCupomNoCarrinho : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CuponsDeDesconto",
                table: "CuponsDeDesconto");

            migrationBuilder.DropColumn(
                name: "Cupom",
                table: "CarrinhosDeCompras");

            migrationBuilder.AlterColumn<string>(
                name: "Cupom",
                table: "CuponsDeDesconto",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "CuponsDeDesconto",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "CupomDeDescontoId",
                table: "CarrinhosDeCompras",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CuponsDeDesconto",
                table: "CuponsDeDesconto",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_CarrinhosDeCompras_CupomDeDescontoId",
                table: "CarrinhosDeCompras",
                column: "CupomDeDescontoId");

            migrationBuilder.AddForeignKey(
                name: "FK_CarrinhosDeCompras_CuponsDeDesconto_CupomDeDescontoId",
                table: "CarrinhosDeCompras",
                column: "CupomDeDescontoId",
                principalTable: "CuponsDeDesconto",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarrinhosDeCompras_CuponsDeDesconto_CupomDeDescontoId",
                table: "CarrinhosDeCompras");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CuponsDeDesconto",
                table: "CuponsDeDesconto");

            migrationBuilder.DropIndex(
                name: "IX_CarrinhosDeCompras_CupomDeDescontoId",
                table: "CarrinhosDeCompras");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "CuponsDeDesconto");

            migrationBuilder.DropColumn(
                name: "CupomDeDescontoId",
                table: "CarrinhosDeCompras");

            migrationBuilder.AlterColumn<string>(
                name: "Cupom",
                table: "CuponsDeDesconto",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Cupom",
                table: "CarrinhosDeCompras",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CuponsDeDesconto",
                table: "CuponsDeDesconto",
                column: "Cupom");
        }
    }
}

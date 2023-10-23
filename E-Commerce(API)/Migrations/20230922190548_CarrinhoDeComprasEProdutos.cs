using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Commerce_API_.Migrations
{
    /// <inheritdoc />
    public partial class CarrinhoDeComprasEProdutos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProdutosNoCarrinho",
                table: "ProdutosNoCarrinho");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ProdutosNoCarrinho");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProdutosNoCarrinho",
                table: "ProdutosNoCarrinho",
                columns: new[] { "CarrinhoDeComprasId", "ProdutoId" });

            migrationBuilder.CreateIndex(
                name: "IX_ProdutosNoCarrinho_ProdutoId",
                table: "ProdutosNoCarrinho",
                column: "ProdutoId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProdutosNoCarrinho_CarrinhosDeCompras_CarrinhoDeComprasId",
                table: "ProdutosNoCarrinho",
                column: "CarrinhoDeComprasId",
                principalTable: "CarrinhosDeCompras",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProdutosNoCarrinho_Produtos_ProdutoId",
                table: "ProdutosNoCarrinho",
                column: "ProdutoId",
                principalTable: "Produtos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProdutosNoCarrinho_CarrinhosDeCompras_CarrinhoDeComprasId",
                table: "ProdutosNoCarrinho");

            migrationBuilder.DropForeignKey(
                name: "FK_ProdutosNoCarrinho_Produtos_ProdutoId",
                table: "ProdutosNoCarrinho");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProdutosNoCarrinho",
                table: "ProdutosNoCarrinho");

            migrationBuilder.DropIndex(
                name: "IX_ProdutosNoCarrinho_ProdutoId",
                table: "ProdutosNoCarrinho");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "ProdutosNoCarrinho",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProdutosNoCarrinho",
                table: "ProdutosNoCarrinho",
                column: "Id");
        }
    }
}

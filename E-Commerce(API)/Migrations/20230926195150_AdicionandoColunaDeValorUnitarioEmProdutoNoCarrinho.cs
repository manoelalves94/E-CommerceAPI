using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Commerce_API_.Migrations
{
    /// <inheritdoc />
    public partial class AdicionandoColunaDeValorUnitarioEmProdutoNoCarrinho : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "ValorUnitario",
                table: "ProdutosNoCarrinho",
                type: "double",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ValorUnitario",
                table: "ProdutosNoCarrinho");
        }
    }
}

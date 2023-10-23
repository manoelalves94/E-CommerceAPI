using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Commerce_API_.Migrations
{
    /// <inheritdoc />
    public partial class CriandoTabelaDeCD : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CentroDeDistribuicao",
                table: "Produtos");

            migrationBuilder.AddColumn<Guid>(
                name: "CentroDeDistribuicaoId",
                table: "Produtos",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.CreateTable(
                name: "CentrosDeDistribuicao",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Nome = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Logradouro = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Numero = table.Column<int>(type: "int", nullable: false),
                    Complemento = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Bairro = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Cidade = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UF = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CEP = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Status = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DataEHoraCriacao = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DataEHoraModificacao = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CentrosDeDistribuicao", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_CentroDeDistribuicaoId",
                table: "Produtos",
                column: "CentroDeDistribuicaoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Produtos_CentrosDeDistribuicao_CentroDeDistribuicaoId",
                table: "Produtos",
                column: "CentroDeDistribuicaoId",
                principalTable: "CentrosDeDistribuicao",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produtos_CentrosDeDistribuicao_CentroDeDistribuicaoId",
                table: "Produtos");

            migrationBuilder.DropTable(
                name: "CentrosDeDistribuicao");

            migrationBuilder.DropIndex(
                name: "IX_Produtos_CentroDeDistribuicaoId",
                table: "Produtos");

            migrationBuilder.DropColumn(
                name: "CentroDeDistribuicaoId",
                table: "Produtos");

            migrationBuilder.AddColumn<string>(
                name: "CentroDeDistribuicao",
                table: "Produtos",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}

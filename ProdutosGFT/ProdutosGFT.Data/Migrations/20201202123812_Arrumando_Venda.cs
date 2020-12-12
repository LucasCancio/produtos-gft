using Microsoft.EntityFrameworkCore.Migrations;

namespace ProdutosGFT.Data.Migrations
{
    public partial class Arrumando_Venda : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vendas_Fornecedores_FornecedorId",
                table: "Vendas");

            migrationBuilder.DropIndex(
                name: "IX_Vendas_FornecedorId",
                table: "Vendas");

            migrationBuilder.DropColumn(
                name: "FornecedorId",
                table: "Vendas");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "FornecedorId",
                table: "Vendas",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vendas_FornecedorId",
                table: "Vendas",
                column: "FornecedorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vendas_Fornecedores_FornecedorId",
                table: "Vendas",
                column: "FornecedorId",
                principalTable: "Fornecedores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

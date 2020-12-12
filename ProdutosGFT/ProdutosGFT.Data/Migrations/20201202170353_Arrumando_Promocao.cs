using Microsoft.EntityFrameworkCore.Migrations;

namespace ProdutosGFT.Data.Migrations
{
    public partial class Arrumando_Promocao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Promocacao",
                table: "Produtos");

            migrationBuilder.AddColumn<bool>(
                name: "Promocao",
                table: "Produtos",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Promocao",
                table: "Produtos");

            migrationBuilder.AddColumn<bool>(
                name: "Promocacao",
                table: "Produtos",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }
    }
}

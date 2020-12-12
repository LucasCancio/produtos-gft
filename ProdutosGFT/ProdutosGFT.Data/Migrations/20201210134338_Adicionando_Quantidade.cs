using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProdutosGFT.Data.Migrations
{
    public partial class Adicionando_Quantidade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Quantidade",
                table: "ProdutosVendas",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Clientes",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "DataAlteracao", "DataCadastro" },
                values: new object[] { new DateTime(2020, 12, 10, 10, 43, 37, 600, DateTimeKind.Local).AddTicks(7146), new DateTime(2020, 12, 10, 10, 43, 37, 601, DateTimeKind.Local).AddTicks(8851) });

            migrationBuilder.UpdateData(
                table: "Clientes",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "DataAlteracao", "DataCadastro" },
                values: new object[] { new DateTime(2020, 12, 10, 10, 43, 37, 606, DateTimeKind.Local).AddTicks(9278), new DateTime(2020, 12, 10, 10, 43, 37, 606, DateTimeKind.Local).AddTicks(9286) });

            migrationBuilder.UpdateData(
                table: "Fornecedores",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "DataAlteracao", "DataCadastro" },
                values: new object[] { new DateTime(2020, 12, 10, 10, 43, 37, 608, DateTimeKind.Local).AddTicks(9124), new DateTime(2020, 12, 10, 10, 43, 37, 608, DateTimeKind.Local).AddTicks(9113) });

            migrationBuilder.UpdateData(
                table: "Fornecedores",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "DataAlteracao", "DataCadastro" },
                values: new object[] { new DateTime(2020, 12, 10, 10, 43, 37, 609, DateTimeKind.Local).AddTicks(835), new DateTime(2020, 12, 10, 10, 43, 37, 609, DateTimeKind.Local).AddTicks(827) });

            migrationBuilder.UpdateData(
                table: "Produtos",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CodigoProduto", "DataAlteracao", "DataCadastro" },
                values: new object[] { "telev-1-2020-12-10", new DateTime(2020, 12, 10, 10, 43, 37, 616, DateTimeKind.Local).AddTicks(5900), new DateTime(2020, 12, 10, 10, 43, 37, 616, DateTimeKind.Local).AddTicks(5924) });

            migrationBuilder.UpdateData(
                table: "Produtos",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CodigoProduto", "DataAlteracao", "DataCadastro" },
                values: new object[] { "notebook-2-2020-12-10", new DateTime(2020, 12, 10, 10, 43, 37, 618, DateTimeKind.Local).AddTicks(4457), new DateTime(2020, 12, 10, 10, 43, 37, 618, DateTimeKind.Local).AddTicks(4466) });

            migrationBuilder.UpdateData(
                table: "Produtos",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CodigoProduto", "DataAlteracao", "DataCadastro" },
                values: new object[] { "mesa-3-2020-12-10", new DateTime(2020, 12, 10, 10, 43, 37, 618, DateTimeKind.Local).AddTicks(4756), new DateTime(2020, 12, 10, 10, 43, 37, 618, DateTimeKind.Local).AddTicks(4758) });

            migrationBuilder.UpdateData(
                table: "ProdutosVendas",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Quantidade",
                value: 1);

            migrationBuilder.UpdateData(
                table: "ProdutosVendas",
                keyColumn: "Id",
                keyValue: 2L,
                column: "Quantidade",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Vendas",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "DataAlteracao", "DataCadastro", "DataCompra" },
                values: new object[] { new DateTime(2020, 12, 10, 10, 43, 37, 620, DateTimeKind.Local).AddTicks(1368), new DateTime(2020, 12, 10, 10, 43, 37, 620, DateTimeKind.Local).AddTicks(1381), new DateTime(2020, 12, 10, 10, 43, 37, 620, DateTimeKind.Local).AddTicks(1383) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantidade",
                table: "ProdutosVendas");

            migrationBuilder.UpdateData(
                table: "Clientes",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "DataAlteracao", "DataCadastro" },
                values: new object[] { new DateTime(2020, 12, 3, 15, 32, 34, 894, DateTimeKind.Local).AddTicks(7407), new DateTime(2020, 12, 3, 15, 32, 34, 894, DateTimeKind.Local).AddTicks(7922) });

            migrationBuilder.UpdateData(
                table: "Clientes",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "DataAlteracao", "DataCadastro" },
                values: new object[] { new DateTime(2020, 12, 3, 15, 32, 34, 899, DateTimeKind.Local).AddTicks(9472), new DateTime(2020, 12, 3, 15, 32, 34, 899, DateTimeKind.Local).AddTicks(9482) });

            migrationBuilder.UpdateData(
                table: "Fornecedores",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "DataAlteracao", "DataCadastro" },
                values: new object[] { new DateTime(2020, 12, 3, 15, 32, 34, 902, DateTimeKind.Local).AddTicks(3566), new DateTime(2020, 12, 3, 15, 32, 34, 902, DateTimeKind.Local).AddTicks(3024) });

            migrationBuilder.UpdateData(
                table: "Fornecedores",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "DataAlteracao", "DataCadastro" },
                values: new object[] { new DateTime(2020, 12, 3, 15, 32, 34, 902, DateTimeKind.Local).AddTicks(5225), new DateTime(2020, 12, 3, 15, 32, 34, 902, DateTimeKind.Local).AddTicks(5211) });

            migrationBuilder.UpdateData(
                table: "Produtos",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CodigoProduto", "DataAlteracao", "DataCadastro" },
                values: new object[] { "telev-1-2020-12-03", new DateTime(2020, 12, 3, 15, 32, 34, 910, DateTimeKind.Local).AddTicks(5517), new DateTime(2020, 12, 3, 15, 32, 34, 910, DateTimeKind.Local).AddTicks(7656) });

            migrationBuilder.UpdateData(
                table: "Produtos",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CodigoProduto", "DataAlteracao", "DataCadastro" },
                values: new object[] { "notebook-2-2020-12-03", new DateTime(2020, 12, 3, 15, 32, 34, 912, DateTimeKind.Local).AddTicks(5632), new DateTime(2020, 12, 3, 15, 32, 34, 912, DateTimeKind.Local).AddTicks(5644) });

            migrationBuilder.UpdateData(
                table: "Produtos",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CodigoProduto", "DataAlteracao", "DataCadastro" },
                values: new object[] { "mesa-3-2020-12-03", new DateTime(2020, 12, 3, 15, 32, 34, 912, DateTimeKind.Local).AddTicks(5900), new DateTime(2020, 12, 3, 15, 32, 34, 912, DateTimeKind.Local).AddTicks(5902) });

            migrationBuilder.UpdateData(
                table: "Vendas",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "DataAlteracao", "DataCadastro", "DataCompra" },
                values: new object[] { new DateTime(2020, 12, 3, 15, 32, 34, 914, DateTimeKind.Local).AddTicks(4653), new DateTime(2020, 12, 3, 15, 32, 34, 914, DateTimeKind.Local).AddTicks(5103), new DateTime(2020, 12, 3, 15, 32, 34, 914, DateTimeKind.Local).AddTicks(5524) });
        }
    }
}

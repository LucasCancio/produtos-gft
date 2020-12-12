using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProdutosGFT.Data.Migrations
{
    public partial class Arrumando_Dados : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Clientes",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "DataAlteracao", "DataCadastro" },
                values: new object[] { new DateTime(2020, 12, 11, 10, 40, 6, 418, DateTimeKind.Local).AddTicks(8682), new DateTime(2020, 12, 11, 10, 40, 6, 417, DateTimeKind.Local).AddTicks(5029) });

            migrationBuilder.UpdateData(
                table: "Clientes",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "DataAlteracao", "DataCadastro" },
                values: new object[] { new DateTime(2020, 12, 11, 10, 40, 6, 419, DateTimeKind.Local).AddTicks(2557), new DateTime(2020, 12, 11, 10, 40, 6, 419, DateTimeKind.Local).AddTicks(2540) });

            migrationBuilder.UpdateData(
                table: "Fornecedores",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "DataAlteracao", "DataCadastro" },
                values: new object[] { new DateTime(2020, 12, 11, 10, 40, 6, 421, DateTimeKind.Local).AddTicks(4899), new DateTime(2020, 12, 11, 10, 40, 6, 421, DateTimeKind.Local).AddTicks(4870) });

            migrationBuilder.UpdateData(
                table: "Fornecedores",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "DataAlteracao", "DataCadastro" },
                values: new object[] { new DateTime(2020, 12, 11, 10, 40, 6, 421, DateTimeKind.Local).AddTicks(5987), new DateTime(2020, 12, 11, 10, 40, 6, 421, DateTimeKind.Local).AddTicks(5977) });

            migrationBuilder.UpdateData(
                table: "Produtos",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CodigoProduto", "DataAlteracao", "DataCadastro" },
                values: new object[] { "88f1ee86c80b41ceb536aab55e633aac", new DateTime(2020, 12, 11, 10, 40, 6, 429, DateTimeKind.Local).AddTicks(8683), new DateTime(2020, 12, 11, 10, 40, 6, 429, DateTimeKind.Local).AddTicks(8644) });

            migrationBuilder.UpdateData(
                table: "Produtos",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CodigoProduto", "DataAlteracao", "DataCadastro" },
                values: new object[] { "bb5b6c571b73425e9876c58727ee0636", new DateTime(2020, 12, 11, 10, 40, 6, 430, DateTimeKind.Local).AddTicks(2641), new DateTime(2020, 12, 11, 10, 40, 6, 430, DateTimeKind.Local).AddTicks(2630) });

            migrationBuilder.UpdateData(
                table: "Produtos",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CodigoProduto", "DataAlteracao", "DataCadastro" },
                values: new object[] { "ae73d0b26ab74014b90f219094f75065", new DateTime(2020, 12, 11, 10, 40, 6, 430, DateTimeKind.Local).AddTicks(2776), new DateTime(2020, 12, 11, 10, 40, 6, 430, DateTimeKind.Local).AddTicks(2773) });

            migrationBuilder.UpdateData(
                table: "Vendas",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "DataAlteracao", "DataCadastro", "DataCompra" },
                values: new object[] { new DateTime(2020, 12, 11, 10, 40, 6, 433, DateTimeKind.Local).AddTicks(3224), new DateTime(2020, 12, 11, 10, 40, 6, 433, DateTimeKind.Local).AddTicks(3207), new DateTime(2020, 12, 11, 10, 40, 6, 433, DateTimeKind.Local).AddTicks(1816) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                table: "Vendas",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "DataAlteracao", "DataCadastro", "DataCompra" },
                values: new object[] { new DateTime(2020, 12, 10, 10, 43, 37, 620, DateTimeKind.Local).AddTicks(1368), new DateTime(2020, 12, 10, 10, 43, 37, 620, DateTimeKind.Local).AddTicks(1381), new DateTime(2020, 12, 10, 10, 43, 37, 620, DateTimeKind.Local).AddTicks(1383) });
        }
    }
}

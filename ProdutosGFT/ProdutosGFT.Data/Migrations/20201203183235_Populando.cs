using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProdutosGFT.Data.Migrations
{
    public partial class Populando : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Clientes",
                columns: new[] { "Id", "DataAlteracao", "DataCadastro", "Documento", "Email", "IsAtivo", "Nome", "Role", "Senha" },
                values: new object[,]
                {
                    { 1L, new DateTime(2020, 12, 3, 15, 32, 34, 894, DateTimeKind.Local).AddTicks(7407), new DateTime(2020, 12, 3, 15, 32, 34, 894, DateTimeKind.Local).AddTicks(7922), "494.853.900-76", "joao@gmail.com", true, "João Costa", 0, "827CCB0EEA8A706C4C34A16891F84E7B" },
                    { 2L, new DateTime(2020, 12, 3, 15, 32, 34, 899, DateTimeKind.Local).AddTicks(9472), new DateTime(2020, 12, 3, 15, 32, 34, 899, DateTimeKind.Local).AddTicks(9482), "713.754.110-04", "admin@gmail.com", true, "Admin da Silva", 1, "827CCB0EEA8A706C4C34A16891F84E7B" }
                });

            migrationBuilder.InsertData(
                table: "Fornecedores",
                columns: new[] { "Id", "Cnpj", "DataAlteracao", "DataCadastro", "IsAtivo", "Nome" },
                values: new object[,]
                {
                    { 1L, "82.310.397/0001-82", new DateTime(2020, 12, 3, 15, 32, 34, 902, DateTimeKind.Local).AddTicks(3566), new DateTime(2020, 12, 3, 15, 32, 34, 902, DateTimeKind.Local).AddTicks(3024), true, "Amazon" },
                    { 2L, "04.915.856/0001-48", new DateTime(2020, 12, 3, 15, 32, 34, 902, DateTimeKind.Local).AddTicks(5225), new DateTime(2020, 12, 3, 15, 32, 34, 902, DateTimeKind.Local).AddTicks(5211), true, "Serraria do Zé" }
                });

            migrationBuilder.InsertData(
                table: "Produtos",
                columns: new[] { "Id", "Categoria", "CodigoProduto", "DataAlteracao", "DataCadastro", "FornecedorId", "Imagem", "IsAtivo", "Nome", "Promocao", "Quantidade", "Valor", "ValorPromo" },
                values: new object[,]
                {
                    { 1L, "Eletrónicos", "telev-1-2020-12-03", new DateTime(2020, 12, 3, 15, 32, 34, 910, DateTimeKind.Local).AddTicks(5517), new DateTime(2020, 12, 3, 15, 32, 34, 910, DateTimeKind.Local).AddTicks(7656), 1L, "televisao.jpg", true, "Televisão", true, 10L, 2000.0, 1450.0 },
                    { 2L, "Eletrónicos", "notebook-2-2020-12-03", new DateTime(2020, 12, 3, 15, 32, 34, 912, DateTimeKind.Local).AddTicks(5632), new DateTime(2020, 12, 3, 15, 32, 34, 912, DateTimeKind.Local).AddTicks(5644), 1L, "notebook.jpg", true, "Notebook", false, 2L, 5000.0, 4000.0 },
                    { 3L, "Moveis", "mesa-3-2020-12-03", new DateTime(2020, 12, 3, 15, 32, 34, 912, DateTimeKind.Local).AddTicks(5900), new DateTime(2020, 12, 3, 15, 32, 34, 912, DateTimeKind.Local).AddTicks(5902), 2L, "mesa.jpg", true, "Mesa", true, 20L, 500.0, 200.0 }
                });

            migrationBuilder.InsertData(
                table: "Vendas",
                columns: new[] { "Id", "ClienteId", "DataAlteracao", "DataCadastro", "DataCompra", "IsAtivo", "TotalCompra" },
                values: new object[] { 1L, 1L, new DateTime(2020, 12, 3, 15, 32, 34, 914, DateTimeKind.Local).AddTicks(4653), new DateTime(2020, 12, 3, 15, 32, 34, 914, DateTimeKind.Local).AddTicks(5103), new DateTime(2020, 12, 3, 15, 32, 34, 914, DateTimeKind.Local).AddTicks(5524), true, 6450.0 });

            migrationBuilder.InsertData(
                table: "ProdutosVendas",
                columns: new[] { "Id", "ProdutoId", "VendaId" },
                values: new object[] { 1L, 1L, 1L });

            migrationBuilder.InsertData(
                table: "ProdutosVendas",
                columns: new[] { "Id", "ProdutoId", "VendaId" },
                values: new object[] { 2L, 2L, 1L });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Clientes",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Produtos",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "ProdutosVendas",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "ProdutosVendas",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Fornecedores",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Produtos",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Produtos",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Vendas",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Clientes",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Fornecedores",
                keyColumn: "Id",
                keyValue: 1L);
        }
    }
}

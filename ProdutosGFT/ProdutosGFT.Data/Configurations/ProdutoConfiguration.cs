using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProdutosGFT.Domain.Entities;

namespace ProdutosGFT.Data.Configurations
{
    public class ProdutoConfiguration : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.HasKey(p => p.Id);
            builder.HasOne(p => p.Fornecedor).WithMany(f => f.Produtos).HasForeignKey("FornecedorId");

            builder.HasData(
                new Produto
                (
                    id: 1,
                    fornecedorId: 1,
                    nome: "Televisão",
                    categoria: "Eletrónicos",
                    imagem: "televisao.jpg",
                    valor: 2000,
                    promocao: true,
                    valorPromo: 1450,
                    quantidade: 10
                ),
                new Produto
                (
                    id: 2,
                    fornecedorId: 1,
                    nome: "Notebook",
                    categoria: "Eletrónicos",
                    imagem: "notebook.jpg",
                    valor: 5000,
                    promocao: false,
                    valorPromo: 4000,
                    quantidade: 2
                ),
                new Produto
                (
                    id: 3,
                    fornecedorId: 2,
                    nome: "Mesa",
                    categoria: "Moveis",
                    imagem: "mesa.jpg",
                    valor: 500,
                    promocao: true,
                    valorPromo: 200,
                    quantidade: 20
                )
            );
        }
    }
}
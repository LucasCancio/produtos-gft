using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProdutosGFT.Domain.Entities;

namespace ProdutosGFT.Data.Configurations
{
    public class FornecedorConfiguration : IEntityTypeConfiguration<Fornecedor>
    {
        public void Configure(EntityTypeBuilder<Fornecedor> builder)
        {
            builder.HasKey(f => f.Id);

            builder.HasData(
               new Fornecedor
               (
                   id: 1,
                   cnpj: "82.310.397/0001-82",
                   nome: "Amazon"
               ),
                new Fornecedor
                (
                    id: 2,
                    cnpj: "04.915.856/0001-48",
                    nome: "Serraria do Zé"
                )
           );
        }
    }
}
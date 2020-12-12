using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProdutosGFT.Domain.Entities;
using ProdutosGFT.Domain.Util.Enums;
using ProdutosGFT.Domain.Util.Helpers;

namespace ProdutosGFT.Data.Configurations
{
    public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.HasKey(c => c.Id);

            builder.HasData(
               new Cliente(
                   id: 1,
                   documento: "494.853.900-76",
                   nome: "João Costa",
                   email: "joao@gmail.com",
                   role: Role.USER,
                   senha: MD5Hash.CreateHash("12345")
               ),
               new Cliente
               (
                   id: 2,
                   documento: "713.754.110-04",
                   nome: "Admin da Silva",
                   email: "admin@gmail.com",
                   role: Role.ADMIN,
                   senha: MD5Hash.CreateHash("12345")
               )
           );

        }
    }
}
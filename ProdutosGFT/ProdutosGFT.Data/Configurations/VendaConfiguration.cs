using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProdutosGFT.Domain.Entities;

namespace ProdutosGFT.Data.Configurations
{
    public class VendaConfiguration : IEntityTypeConfiguration<Venda>
    {
        public void Configure(EntityTypeBuilder<Venda> builder)
        {
            builder.HasKey(v => v.Id);
            builder.HasOne(v => v.Cliente).WithMany().HasForeignKey("ClienteId");

            builder.HasData(
                new Venda
                (
                    id: 1,
                    clienteId: 1,
                    dataCompra: DateTime.Now,
                    totalCompra: 6450
                )
            ); 

        }
    }
}
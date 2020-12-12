using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProdutosGFT.Domain.Entities;

namespace ProdutosGFT.Data.Configurations
{
    public class ProdutoVendaConfiguration : IEntityTypeConfiguration<ProdutoVenda>
    {
        public void Configure(EntityTypeBuilder<ProdutoVenda> builder)
        {
            builder.HasKey(pv => pv.Id);
            builder.HasOne(pv => pv.Produto).WithMany().HasForeignKey("ProdutoId");
            builder.HasOne(pv => pv.Venda).WithMany().HasForeignKey("VendaId");

            builder.HasData(
                new ProdutoVenda
                (
                    id: 1,
                    produtoId: 1,
                    vendaId: 1
                ),
                new ProdutoVenda
                (
                    id: 2,
                    produtoId: 2,
                    vendaId: 1
                )
            );
        }
    }
}
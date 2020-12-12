using Microsoft.EntityFrameworkCore;
using ProdutosGFT.Data.Configurations;
using ProdutosGFT.Domain.Entities;
using Microsoft.Extensions.Logging;
using System;

namespace ProdutosGFT.Data
{
    public class ProdutosGFTDbContext : DbContext
    {
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Fornecedor> Fornecedores { get; set; }
        public DbSet<Venda> Vendas { get; set; }
        public DbSet<ProdutoVenda> ProdutosVendas { get; set; }

        private readonly ILoggerFactory _loggerFactory;
        public ProdutosGFTDbContext(DbContextOptions<ProdutosGFTDbContext> options, ILoggerFactory loggerFactory)
          : base(options)
        {
            _loggerFactory = loggerFactory;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(_loggerFactory);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ClienteConfiguration());
            modelBuilder.ApplyConfiguration(new FornecedorConfiguration());
            modelBuilder.ApplyConfiguration(new ProdutoConfiguration());
            modelBuilder.ApplyConfiguration(new VendaConfiguration());
            modelBuilder.ApplyConfiguration(new ProdutoVendaConfiguration());
        }
    }
}
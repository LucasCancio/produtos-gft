﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProdutosGFT.Data;

namespace ProdutosGFT.Data.Migrations
{
    [DbContext(typeof(ProdutosGFTDbContext))]
    [Migration("20201203183235_Populando")]
    partial class Populando
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("ProdutosGFT.Domain.Models.Cliente", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<DateTime>("DataAlteracao")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DataCadastro")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Documento")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Email")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("IsAtivo")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Nome")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<string>("Senha")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("Clientes");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            DataAlteracao = new DateTime(2020, 12, 3, 15, 32, 34, 894, DateTimeKind.Local).AddTicks(7407),
                            DataCadastro = new DateTime(2020, 12, 3, 15, 32, 34, 894, DateTimeKind.Local).AddTicks(7922),
                            Documento = "494.853.900-76",
                            Email = "joao@gmail.com",
                            IsAtivo = true,
                            Nome = "João Costa",
                            Role = 0,
                            Senha = "827CCB0EEA8A706C4C34A16891F84E7B"
                        },
                        new
                        {
                            Id = 2L,
                            DataAlteracao = new DateTime(2020, 12, 3, 15, 32, 34, 899, DateTimeKind.Local).AddTicks(9472),
                            DataCadastro = new DateTime(2020, 12, 3, 15, 32, 34, 899, DateTimeKind.Local).AddTicks(9482),
                            Documento = "713.754.110-04",
                            Email = "admin@gmail.com",
                            IsAtivo = true,
                            Nome = "Admin da Silva",
                            Role = 1,
                            Senha = "827CCB0EEA8A706C4C34A16891F84E7B"
                        });
                });

            modelBuilder.Entity("ProdutosGFT.Domain.Models.Fornecedor", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("Cnpj")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("DataAlteracao")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DataCadastro")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsAtivo")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Nome")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("Fornecedores");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Cnpj = "82.310.397/0001-82",
                            DataAlteracao = new DateTime(2020, 12, 3, 15, 32, 34, 902, DateTimeKind.Local).AddTicks(3566),
                            DataCadastro = new DateTime(2020, 12, 3, 15, 32, 34, 902, DateTimeKind.Local).AddTicks(3024),
                            IsAtivo = true,
                            Nome = "Amazon"
                        },
                        new
                        {
                            Id = 2L,
                            Cnpj = "04.915.856/0001-48",
                            DataAlteracao = new DateTime(2020, 12, 3, 15, 32, 34, 902, DateTimeKind.Local).AddTicks(5225),
                            DataCadastro = new DateTime(2020, 12, 3, 15, 32, 34, 902, DateTimeKind.Local).AddTicks(5211),
                            IsAtivo = true,
                            Nome = "Serraria do Zé"
                        });
                });

            modelBuilder.Entity("ProdutosGFT.Domain.Models.Produto", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("Categoria")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("CodigoProduto")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("DataAlteracao")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DataCadastro")
                        .HasColumnType("datetime(6)");

                    b.Property<long>("FornecedorId")
                        .HasColumnType("bigint");

                    b.Property<string>("Imagem")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("IsAtivo")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Nome")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("Promocao")
                        .HasColumnType("tinyint(1)");

                    b.Property<long>("Quantidade")
                        .HasColumnType("bigint");

                    b.Property<double>("Valor")
                        .HasColumnType("double");

                    b.Property<double>("ValorPromo")
                        .HasColumnType("double");

                    b.HasKey("Id");

                    b.HasIndex("FornecedorId");

                    b.ToTable("Produtos");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Categoria = "Eletrónicos",
                            CodigoProduto = "telev-1-2020-12-03",
                            DataAlteracao = new DateTime(2020, 12, 3, 15, 32, 34, 910, DateTimeKind.Local).AddTicks(5517),
                            DataCadastro = new DateTime(2020, 12, 3, 15, 32, 34, 910, DateTimeKind.Local).AddTicks(7656),
                            FornecedorId = 1L,
                            Imagem = "televisao.jpg",
                            IsAtivo = true,
                            Nome = "Televisão",
                            Promocao = true,
                            Quantidade = 10L,
                            Valor = 2000.0,
                            ValorPromo = 1450.0
                        },
                        new
                        {
                            Id = 2L,
                            Categoria = "Eletrónicos",
                            CodigoProduto = "notebook-2-2020-12-03",
                            DataAlteracao = new DateTime(2020, 12, 3, 15, 32, 34, 912, DateTimeKind.Local).AddTicks(5632),
                            DataCadastro = new DateTime(2020, 12, 3, 15, 32, 34, 912, DateTimeKind.Local).AddTicks(5644),
                            FornecedorId = 1L,
                            Imagem = "notebook.jpg",
                            IsAtivo = true,
                            Nome = "Notebook",
                            Promocao = false,
                            Quantidade = 2L,
                            Valor = 5000.0,
                            ValorPromo = 4000.0
                        },
                        new
                        {
                            Id = 3L,
                            Categoria = "Moveis",
                            CodigoProduto = "mesa-3-2020-12-03",
                            DataAlteracao = new DateTime(2020, 12, 3, 15, 32, 34, 912, DateTimeKind.Local).AddTicks(5900),
                            DataCadastro = new DateTime(2020, 12, 3, 15, 32, 34, 912, DateTimeKind.Local).AddTicks(5902),
                            FornecedorId = 2L,
                            Imagem = "mesa.jpg",
                            IsAtivo = true,
                            Nome = "Mesa",
                            Promocao = true,
                            Quantidade = 20L,
                            Valor = 500.0,
                            ValorPromo = 200.0
                        });
                });

            modelBuilder.Entity("ProdutosGFT.Domain.Models.ProdutoVenda", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<long>("ProdutoId")
                        .HasColumnType("bigint");

                    b.Property<long>("VendaId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("ProdutoId");

                    b.HasIndex("VendaId");

                    b.ToTable("ProdutosVendas");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            ProdutoId = 1L,
                            VendaId = 1L
                        },
                        new
                        {
                            Id = 2L,
                            ProdutoId = 2L,
                            VendaId = 1L
                        });
                });

            modelBuilder.Entity("ProdutosGFT.Domain.Models.Venda", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<long>("ClienteId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("DataAlteracao")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DataCadastro")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DataCompra")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsAtivo")
                        .HasColumnType("tinyint(1)");

                    b.Property<double>("TotalCompra")
                        .HasColumnType("double");

                    b.HasKey("Id");

                    b.HasIndex("ClienteId");

                    b.ToTable("Vendas");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            ClienteId = 1L,
                            DataAlteracao = new DateTime(2020, 12, 3, 15, 32, 34, 914, DateTimeKind.Local).AddTicks(4653),
                            DataCadastro = new DateTime(2020, 12, 3, 15, 32, 34, 914, DateTimeKind.Local).AddTicks(5103),
                            DataCompra = new DateTime(2020, 12, 3, 15, 32, 34, 914, DateTimeKind.Local).AddTicks(5524),
                            IsAtivo = true,
                            TotalCompra = 6450.0
                        });
                });

            modelBuilder.Entity("ProdutosGFT.Domain.Models.Produto", b =>
                {
                    b.HasOne("ProdutosGFT.Domain.Models.Fornecedor", "Fornecedor")
                        .WithMany("Produtos")
                        .HasForeignKey("FornecedorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProdutosGFT.Domain.Models.ProdutoVenda", b =>
                {
                    b.HasOne("ProdutosGFT.Domain.Models.Produto", "Produto")
                        .WithMany()
                        .HasForeignKey("ProdutoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProdutosGFT.Domain.Models.Venda", "Venda")
                        .WithMany()
                        .HasForeignKey("VendaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProdutosGFT.Domain.Models.Venda", b =>
                {
                    b.HasOne("ProdutosGFT.Domain.Models.Cliente", "Cliente")
                        .WithMany()
                        .HasForeignKey("ClienteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}

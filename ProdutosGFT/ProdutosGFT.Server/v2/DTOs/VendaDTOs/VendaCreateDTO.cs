using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using FluentValidation.Results;
using ProdutosGFT.Domain.Entities;
using ProdutosGFT.Domain.Interfaces.Generics.Entities;
using ProdutosGFT.Domain.Interfaces.Repositories;
using ProdutosGFT.Server.v2.Validators;

namespace ProdutosGFT.Server.v2.DTOs.VendaDTOs
{
    public class VendaCreateDTO : IDefaultDTO<Venda>
    {
        public ProdutoQtdCreateDTO[] Produtos { get; set; }

        public long ClienteId { get; set; }
        [JsonIgnore]
        public Cliente Cliente { get; set; }

        public DateTime DataCompra { get; set; }


        #region Validação

        [JsonIgnore]
        public IList<ValidationFailure> Errors { get; set; }

        public virtual bool IsValid()
        {
            var validator = new VendaCreateValidator();

            ValidationResult result = validator.Validate(this);
            this.Errors = result.Errors;

            return result.IsValid;
        }

        #endregion

        public async Task FixDTO(IClienteRepository clienteRepository, IProdutoRepository produtoRepository)
        {
            this.Cliente = await clienteRepository.SelectByIdAsync(this.ClienteId);

            foreach (var produtoQtd in Produtos)
            {
                produtoQtd.Produto = await produtoRepository.SelectByIdAsync(produtoQtd.Id);
            }
        }

        public Venda ToModel(long id = 0)
        {
            double totalCompra = this.Produtos.Sum(produtoQtd =>
            {
                Produto produto = produtoQtd.Produto;

                bool isAtPromocao = produto.Promocao;
                if (isAtPromocao) return produto.ValorPromo * produtoQtd.Quantidade;
                return produto.Valor * produtoQtd.Quantidade;
            });

            return new Venda
            (
                id: id,
                cliente: this.Cliente,
                dataCompra: this.DataCompra,
                totalCompra: totalCompra
            );
        }
    }
}
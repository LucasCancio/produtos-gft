using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using FluentValidation.Results;
using ProdutosGFT.Domain.Entities;
using ProdutosGFT.Domain.Interfaces.Generics.Entities;
using ProdutosGFT.Domain.Interfaces.Repositories;
using ProdutosGFT.Server.v1.Validators;

namespace ProdutosGFT.Server.v1.DTOs.VendaDTOs
{
    public class VendaCreateDTO : IDefaultDTO<Venda>
    {
        public List<long> FornecedoresIds { get; set; }

        public List<long> ProdutosIds { get; set; }
        [JsonIgnore]
        public List<Produto> Produtos { get; set; } = new List<Produto>();

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

            foreach (long id in this.ProdutosIds)
            {
                Produto produto = await produtoRepository.SelectByIdAsync(id);
                this.Produtos.Add(produto);
            }
        }

        public Venda ToModel(long id = 0)
        {
            double totalCompra = this.Produtos.Sum(p =>
            {
                bool isAtPromocao = p.Promocao;
                if (isAtPromocao) return p.ValorPromo;
                return p.Valor;
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
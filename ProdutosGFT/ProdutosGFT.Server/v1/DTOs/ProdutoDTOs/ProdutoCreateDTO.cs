using System.Collections.Generic;
using System.Text.Json.Serialization;
using FluentValidation.Results;
using ProdutosGFT.Domain.Entities;
using ProdutosGFT.Domain.Interfaces.Generics.Entities;
using ProdutosGFT.Server.v1.Validators;

namespace ProdutosGFT.Server.v1.DTOs.ProdutoDTOs
{
    public class ProdutoCreateDTO : IDefaultDTO<Produto>
    {
        /// <example>Geladeira</example>
        public string Nome { get; set; }
        /// <example>1000</example>
        public double? Valor { get; set; }
        /// <example>true</example>
        public bool? Promocao { get; set; }
        /// <example>500</example>
        public double? ValorPromo { get; set; }
        /// <example>Eletrodomésticos</example>
        public string Categoria { get; set; }
        /// <example>geladeira.png</example>
        public string Imagem { get; set; }
        /// <example>10</example>
        public long? Quantidade { get; set; }
        public long? FornecedorId { get; set; }

        #region Validação

        [JsonIgnore]
        public IList<ValidationFailure> Errors { get; set; }

        public virtual bool IsValid()
        {
            var validator = new ProdutoCreateValidator();

            ValidationResult result = validator.Validate(this);
            this.Errors = result.Errors;

            return result.IsValid;
        }

        #endregion

        public virtual Produto ToModel(long id = 0)
        {
            return new Produto
            (
                id: id,
                categoria: this.Categoria,
                fornecedorId: this.FornecedorId.Value,
                imagem: this.Imagem,
                nome: this.Nome,
                promocao: this.Promocao.Value,
                quantidade: this.Quantidade.Value,
                valor: this.Valor.Value,
                valorPromo: this.ValorPromo.Value
            );
        }
    }
}
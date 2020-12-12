using System.Collections.Generic;
using System.Text.Json.Serialization;
using FluentValidation.Results;
using ProdutosGFT.Domain.Entities;
using ProdutosGFT.Domain.Interfaces.Generics.Entities;
using ProdutosGFT.Server.v1.Validators;

namespace ProdutosGFT.Server.v1.DTOs.FornecedorDTOs
{
    public class FornecedorCreateDTO : IDefaultDTO<Fornecedor>
    {
        /// <example>Lojinha do seu Zé</example>
        public string Nome { get; set; }
        /// <example>99.999.999/9999-99</example>
        public string Cnpj { get; set; }

        #region Validação

        [JsonIgnore]
        public IList<ValidationFailure> Errors { get; set; }

        public virtual bool IsValid()
        {
            var validator = new FornecedorCreateValidator();

            ValidationResult result = validator.Validate(this);
            this.Errors = result.Errors;

            return result.IsValid;
        }

        #endregion

        public virtual Fornecedor ToModel(long id = 0)
        {
            return new Fornecedor
            (
                id: id,
                cnpj: this.Cnpj,
                nome: this.Nome
            );
        }
    }
}
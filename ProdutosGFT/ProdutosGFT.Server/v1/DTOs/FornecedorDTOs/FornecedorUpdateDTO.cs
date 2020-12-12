using FluentValidation.Results;
using ProdutosGFT.Domain.Entities;
using ProdutosGFT.Server.v1.Validators;

namespace ProdutosGFT.Server.v1.DTOs.FornecedorDTOs
{
    public class FornecedorUpdateDTO : FornecedorCreateDTO
    {
        public long Id { get; set; }

        #region Validação
        public bool IsValid(bool isPatch = false)
        {
            var validator = new FornecedorUpdateValidator(isPatch);

            ValidationResult result = validator.Validate(this);
            this.Errors = result.Errors;

            return result.IsValid;
        }

        #endregion
        public Fornecedor ToModel()
        {
            var fornecedor = base.ToModel(id: this.Id);
            return fornecedor;
        }
    }
}
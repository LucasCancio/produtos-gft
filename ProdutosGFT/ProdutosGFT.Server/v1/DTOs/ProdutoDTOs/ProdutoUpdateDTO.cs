using FluentValidation.Results;
using ProdutosGFT.Domain.Entities;
using ProdutosGFT.Server.v1.Validators;

namespace ProdutosGFT.Server.v1.DTOs.ProdutoDTOs
{
    public class ProdutoUpdateDTO : ProdutoCreateDTO
    {
        public long Id { get; set; }

        #region Validação

        public bool IsValid(bool isPatch = false)
        {
            var validator = new ProdutoUpdateValidator(isPatch);

            ValidationResult result = validator.Validate(this);
            this.Errors = result.Errors;

            return result.IsValid;
        }

        #endregion

        public Produto ToModel()
        {
            var produto = base.ToModel(id: this.Id);
            return produto;
        }
    }
}
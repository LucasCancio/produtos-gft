using FluentValidation.Results;
using ProdutosGFT.Domain.Entities;
using ProdutosGFT.Server.v1.Validators;

namespace ProdutosGFT.Server.v1.DTOs.ClienteDTOs
{
    public class ClienteUpdateDTO : ClienteCreateDTO
    {
        public long Id { get; set; }

        #region Validação

        public bool IsValid(bool isPatch = false)
        {
            var validator = new ClienteUpdateValidator(isPatch);

            ValidationResult result = validator.Validate(this);
            this.Errors = result.Errors;

            return result.IsValid;
        }

        #endregion

        public Cliente ToModel()
        {
            var cliente = base.ToModel(id: this.Id);
            return cliente;
        }
    }
}
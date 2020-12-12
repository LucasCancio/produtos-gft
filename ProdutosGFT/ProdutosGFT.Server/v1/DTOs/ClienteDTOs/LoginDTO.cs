using System.Collections.Generic;
using System.Text.Json.Serialization;
using FluentValidation.Results;
using ProdutosGFT.Server.v1.Validators;

namespace ProdutosGFT.Server.v1.DTOs.ClienteDTOs
{
    public class LoginDTO
    {
        /// <example>email@gmail.com</example>
        public string Email { get; set; }
        /// <example>12345</example>
        public string Senha { get; set; }

        #region Validação

        [JsonIgnore]
        public IList<ValidationFailure> Errors { get; set; }

        public bool IsValid()
        {
            var validator = new LoginValidator();

            ValidationResult result = validator.Validate(this);
            this.Errors = result.Errors;

            return result.IsValid;
        }

        #endregion
    }
}
using System.Collections.Generic;
using System.Text.Json.Serialization;
using FluentValidation.Results;
using ProdutosGFT.Domain.Entities;
using ProdutosGFT.Domain.Interfaces.Generics.Entities;
using ProdutosGFT.Domain.Util.Enums;
using ProdutosGFT.Domain.Util.Helpers;
using ProdutosGFT.Server.v1.Validators;

namespace ProdutosGFT.Server.v1.DTOs.ClienteDTOs
{
    public class ClienteCreateDTO : IDefaultDTO<Cliente>
    {
        /// <example>Claudio Luis</example>
        public string Nome { get; set; }
        /// <example>claudio@gmail.com</example>
        public string Email { get; set; }
        /// <example>claudio12345</example>
        public string Senha { get; set; }
        /// <example>999.999.999-99</example>
        public string Documento { get; set; }
        /// <example>USER</example>
        public Role? Role { get; set; }

        #region Validação

        [JsonIgnore]
        public IList<ValidationFailure> Errors { get; set; }

        public virtual bool IsValid()
        {
            var validator = new ClienteCreateValidator();

            ValidationResult result = validator.Validate(this);
            this.Errors = result.Errors;

            return result.IsValid;
        }

        #endregion

        public virtual Cliente ToModel(long id = 0)
        {
            return new Cliente(
                id: id,
                documento: this.Documento,
                email: this.Email,
                nome: this.Nome,
                role: this.Role.Value,
                senha: MD5Hash.CreateHash(this.Senha)
            );
        }
    }
}
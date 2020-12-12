using FluentValidation;
using ProdutosGFT.Server.v1.DTOs.ClienteDTOs;

namespace ProdutosGFT.Server.v1.Validators
{
    public class LoginValidator : AbstractValidator<LoginDTO>
    {
        public LoginValidator()
        {
            RuleFor(c => c.Email)
                            .NotEmpty()
                            .EmailAddress();

            RuleFor(c => c.Senha)
                            .NotEmpty()
                            .Length(5, 20);
        }
    }
}
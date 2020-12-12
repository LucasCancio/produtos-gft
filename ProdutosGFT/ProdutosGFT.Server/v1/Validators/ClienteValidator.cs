using FluentValidation;
using ProdutosGFT.Server.v1.DTOs.ClienteDTOs;
using ProdutosGFT.Domain.Util.Helpers;

namespace ProdutosGFT.Server.v1.Validators
{
    public class ClienteCreateValidator : AbstractValidator<ClienteCreateDTO>
    {
        public ClienteCreateValidator()
        {
            RuleFor(c => c.Nome)
                            .NotEmpty()
                            .Length(5, 100);

            RuleFor(c => c.Documento)
                            .NotEmpty()
                            .Length(14)
                            .WithMessage("'{PropertyName}' deve ter {MaxLength} caracteres. Você digitou {TotalLength} caracteres.")
                            .Must(CPFValidator.IsCpf)
                            .WithMessage("'{PropertyName}' deve ser um CPF válido.");

            RuleFor(c => c.Email)
                            .NotEmpty()
                            .EmailAddress();

            RuleFor(c => c.Senha)
                            .NotEmpty()
                            .Length(5, 20);

            RuleFor(c => c.Role)
                            .NotEmpty()
                            .IsInEnum();
        }
    }

    public class ClienteUpdateValidator : AbstractValidator<ClienteUpdateDTO>
    {
        public ClienteUpdateValidator(bool isPatch = false)
        {
            RuleFor(c => c.Id)
                            .NotNull()
                            .GreaterThan(0);

            if (!isPatch)
            {
                RuleFor(c => c.Nome)
                            .NotEmpty();

                RuleFor(c => c.Documento)
                            .NotEmpty();

                RuleFor(c => c.Email)
                            .NotEmpty();

                RuleFor(c => c.Senha)
                            .NotEmpty();

                RuleFor(c => c.Role)
                            .NotEmpty();
            }

            RuleFor(c => c.Nome)
                            .Length(5, 100)
                            .When(c => !string.IsNullOrEmpty(c.Nome));

            RuleFor(c => c.Documento)
                            .Length(14)
                            .WithMessage("'{PropertyName}' deve ter {MaxLength} caracteres. Você digitou {TotalLength} caracteres.")
                            .Must(CPFValidator.IsCpf)
                            .WithMessage("'{PropertyName}' deve ser um CPF válido.")
                            .When(c => !string.IsNullOrEmpty(c.Documento));

            RuleFor(c => c.Email)
                            .EmailAddress()
                            .When(c => !string.IsNullOrEmpty(c.Email));

            RuleFor(c => c.Senha)
                            .Length(5, 20)
                            .When(c => !string.IsNullOrEmpty(c.Senha));

            RuleFor(c => c.Role)
                            .IsInEnum()
                            .When(c => c.Role.HasValue);
        }
    }
}
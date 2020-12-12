using FluentValidation;
using ProdutosGFT.Server.v1.DTOs.FornecedorDTOs;
using ProdutosGFT.Domain.Util.Helpers;

namespace ProdutosGFT.Server.v1.Validators
{
    public class FornecedorCreateValidator : AbstractValidator<FornecedorCreateDTO>
    {
        public FornecedorCreateValidator()
        {
            RuleFor(f => f.Nome)
                           .NotEmpty()
                           .Length(5, 100);

            RuleFor(f => f.Cnpj)
                           .NotEmpty()
                           .Length(18)
                           .WithMessage("'{PropertyName}' deve ter {MaxLength} caracteres. Você digitou {TotalLength} caracteres.")
                           .Must(CNPJValidator.IsCnpj)
                           .WithMessage("'{PropertyName}' deve ser um CNPJ válido.");
        }
    }

    public class FornecedorUpdateValidator : AbstractValidator<FornecedorUpdateDTO>
    {
        public FornecedorUpdateValidator(bool isPatch = false)
        {
            RuleFor(f => f.Id)
                            .NotNull()
                            .GreaterThan(0);

            if (!isPatch)
            {
                RuleFor(f => f.Nome)
                        .NotEmpty();

                RuleFor(f => f.Cnpj)
                        .NotEmpty();
            }

            RuleFor(f => f.Nome)
                            .Length(5, 100)
                            .When(f => !string.IsNullOrEmpty(f.Nome));

            RuleFor(f => f.Cnpj)
                           .Length(18)
                           .WithMessage("'{PropertyName}' deve ter {MaxLength} caracteres. Você digitou {TotalLength} caracteres.")
                           .Must(CNPJValidator.IsCnpj)
                           .WithMessage("'{PropertyName}' deve ser um CNPJ válido.")
                           .When(f => !string.IsNullOrEmpty(f.Cnpj));
        }
    }
}

using FluentValidation;
using ProdutosGFT.Server.v1.DTOs.ProdutoDTOs;

namespace ProdutosGFT.Server.v1.Validators
{
    public class ProdutoCreateValidator : AbstractValidator<ProdutoCreateDTO>
    {
        public ProdutoCreateValidator()
        {
            RuleFor(p => p.Nome)
                           .NotEmpty()
                           .Length(5, 100);

            RuleFor(p => p.Quantidade)
                           .NotEmpty()
                           .GreaterThanOrEqualTo(0);

            RuleFor(p => p.Valor)
                           .NotEmpty()
                           .GreaterThan(0);

            RuleFor(p => p.ValorPromo)
                           .GreaterThanOrEqualTo(0)
                           .LessThan(p => p.Valor);

            RuleFor(p => p.Imagem)
                           .NotEmpty()
                           .Length(4, 100);
        }
    }

    public class ProdutoUpdateValidator : AbstractValidator<ProdutoUpdateDTO>
    {
        public ProdutoUpdateValidator(bool isPatch = false)
        {
            RuleFor(p => p.Id)
                            .NotNull()
                            .GreaterThan(0);

            if (!isPatch)
            {
                RuleFor(p => p.Nome)
                        .NotEmpty();

                RuleFor(p => p.Quantidade)
                        .NotNull();

                RuleFor(p => p.Valor)
                        .NotNull();

                RuleFor(p => p.ValorPromo)
                        .NotNull();

                RuleFor(p => p.Imagem)
                        .NotEmpty();
            }

            RuleFor(p => p.Nome)
                          .Length(5, 100)
                          .When(p => !string.IsNullOrEmpty(p.Nome));

            RuleFor(p => p.Quantidade)
                           .GreaterThan(0)
                           .When(p => p.Quantidade.HasValue);

            RuleFor(p => p.Valor)
                           .GreaterThan(0)
                           .When(p => p.Valor.HasValue);

            RuleFor(p => p.ValorPromo)
                           .GreaterThanOrEqualTo(0)
                           .LessThan(p => p.Valor)
                           .When(p => p.ValorPromo.HasValue && p.Promocao.Value);

            RuleFor(p => p.Imagem)
                           .Length(4, 100)
                           .When(p => !string.IsNullOrEmpty(p.Imagem));
        }
    }
}
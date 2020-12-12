using System;
using FluentValidation;
using ProdutosGFT.Server.v2.DTOs.VendaDTOs;

namespace ProdutosGFT.Server.v2.Validators
{
    public class VendaCreateValidator : AbstractValidator<VendaCreateDTO>
    {
        public VendaCreateValidator()
        {
            RuleFor(v => v.ClienteId)
                            .GreaterThanOrEqualTo(0);

            RuleFor(v => v.Produtos)
                            .NotEmpty();

            RuleForEach(v => v.Produtos)
                            .SetValidator(new ProdutoQtdValidator());
                            
            RuleFor(v => v.DataCompra)
                            .NotEmpty()
                            .LessThan(DateTime.Today.AddDays(1)).WithMessage("A data da compra não pode ser uma data futura.");
                            
        }
    }
}
using System;
using FluentValidation;
using ProdutosGFT.Server.v1.DTOs.VendaDTOs;

namespace ProdutosGFT.Server.v1.Validators
{
    public class VendaCreateValidator : AbstractValidator<VendaCreateDTO>
    {
        public VendaCreateValidator()
        {
            RuleFor(v => v.ClienteId)
                            .GreaterThanOrEqualTo(0);

            RuleFor(v => v.ProdutosIds)
                            .NotEmpty();

            RuleFor(v => v.FornecedoresIds)
                            .NotEmpty();

            RuleFor(v => v.DataCompra)
                            .NotEmpty()
                            .LessThan(DateTime.Today.AddDays(1)).WithMessage("A data da compra não pode ser uma data futura.");
                            
        }
    }
}
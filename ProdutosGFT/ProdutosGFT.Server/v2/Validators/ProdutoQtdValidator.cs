using FluentValidation;
using ProdutosGFT.Server.v2.DTOs.VendaDTOs;

namespace ProdutosGFT.Server.v2.Validators
{
    public class ProdutoQtdValidator : AbstractValidator<ProdutoQtdCreateDTO>
    {
        public ProdutoQtdValidator()
        {
            RuleFor(p => p.Quantidade)
                            .GreaterThan(0);

            RuleFor(p => p.Id)
                            .GreaterThan(0);
        }
    }
}
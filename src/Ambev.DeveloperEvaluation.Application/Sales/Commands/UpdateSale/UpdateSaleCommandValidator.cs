using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.Commands.UpdateSale;

public class UpdateSaleCommandValidator : AbstractValidator<UpdateSaleCommand>
{
    public UpdateSaleCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleForEach(x => x.Items.Where(i => !i.IsDeleted)).ChildRules(item =>
        {
            item.RuleFor(i => i.ProductId).NotEmpty();
            item.RuleFor(i => i.ProductName).NotEmpty().MaximumLength(100);
            item.RuleFor(i => i.Quantity).InclusiveBetween(1, 20);
            item.RuleFor(i => i.UnitPrice).GreaterThan(0);
        });
    }
}
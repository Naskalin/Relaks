using FluentValidation;
using Relaks.Views.Pages.Financials.ViewModels;

namespace Relaks.Validators.FinancialValidators;

public class FinancialTransactionItemRequestValidator : AbstractValidator<FinancialTransactionItemRequest>
{
    public FinancialTransactionItemRequestValidator()
    {
        RuleFor(x => x.CategoryId).Must(categoryId => categoryId != default);
        RuleFor(x => x.Quantity).NotEmpty().GreaterThan(0);
        RuleFor(x => x.Amount).NotEmpty().GreaterThan(0);
        RuleFor(x => x.Description).MaximumLength(500);
    }
}
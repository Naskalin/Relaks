using FluentValidation;
using Relaks.Views.Pages.Financials.ViewModels;

namespace Relaks.Validators.FinancialValidators;

public class FinancialAccountRequestValidator : AbstractValidator<FinancialAccountRequest>
{
    public FinancialAccountRequestValidator()
    {
        RuleFor(x => x.Title).NotEmpty().MaximumLength(255);
        RuleFor(x => x.Description).MaximumLength(500);
        RuleFor(x => x.StartAt).Must(x => x != default);
        When(x => x.EndAt.HasValue, () =>
        {
            RuleFor(x => x.EndAt!.Value).GreaterThan(x => x.StartAt);
        });
        RuleFor(x => x.CategoryId).NotEmpty();
        RuleFor(x => x.FinancialCurrencyId).NotEmpty();
    }
}
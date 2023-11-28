using FluentValidation;
using Relaks.Views.Pages.Financials.ViewModels;

namespace Relaks.Validators.FinancialValidators;

public class FinancialAccountCategoryRequestValidator : AbstractValidator<FinancialAccountCategoryRequest>
{
    public FinancialAccountCategoryRequestValidator()
    {
        RuleFor(x => x.Title).NotEmpty().MaximumLength(255);
    }
}
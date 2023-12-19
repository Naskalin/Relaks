using FluentValidation;
using Relaks.Views.Pages.Financials.ViewModels;

namespace Relaks.Validators.FinancialValidators;

public class FinancialAccountCategoryRequestValidator : AbstractValidator<FinancialAccountCategoryRequest>
{
    public FinancialAccountCategoryRequestValidator()
    {
        RuleFor(x => x.Title).NotEmpty().MaximumLength(255);
        RuleFor(x => x.EntryId).Must(entryId => entryId.HasValue && entryId.Value != default);
    }
}
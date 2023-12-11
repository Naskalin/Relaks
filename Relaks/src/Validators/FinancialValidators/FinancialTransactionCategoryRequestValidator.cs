using FluentValidation;
using Relaks.Database;
using Relaks.Models.FinancialModels;
using Relaks.Views.Pages.Financials.ViewModels;

namespace Relaks.Validators.FinancialValidators;

public class FinancialTransactionCategoryRequestValidator : AbstractValidator<FinancialTransactionCategoryRequest>
{
    public FinancialTransactionCategoryRequestValidator(AppDbContext db)
    {
        RuleFor(x => x.Title).NotEmpty().MaximumLength(255);
        RuleFor(x => x).SetValidator(new TreeNodeValidator<FinancialTransactionCategoryRequest, FinancialTransactionCategory>(db));
    }
}
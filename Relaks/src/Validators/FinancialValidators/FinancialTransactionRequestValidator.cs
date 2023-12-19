using FluentValidation;
using Relaks.Views.Pages.EntryFinancials.ViewModels;

namespace Relaks.Validators.FinancialValidators;

public class FinancialTransactionRequestValidator : AbstractValidator<FinancialTransactionRequest>
{
    public FinancialTransactionRequestValidator()
    {
        RuleFor(x => x.IsPlus).NotNull();
        RuleFor(x => x.CreatedAt).NotEmpty().Must(x => x != default);
        RuleFor(x => x.Description).MaximumLength(500);
        RuleFor(x => x.AccountId).NotEmpty().Must(accountId => accountId != default);
        RuleFor(x => x.EntryId).Must(entryId => entryId.HasValue && entryId.Value != default);
        RuleFor(x => x.Items).NotEmpty().Must(items => items.Any());
        
        RuleForEach(x => x.Items).SetValidator(new FinancialTransactionItemRequestValidator());
    }
}
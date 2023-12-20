using FluentValidation;
using Relaks.Views.Pages.EntryFinancials.ViewModels;

namespace Relaks.Validators.FinancialValidators;

public class BaseFinancialTransactionRequestValidator : AbstractValidator<BaseFinancialTransactionRequest>
{
    public BaseFinancialTransactionRequestValidator()
    {
        RuleFor(x => x.IsPlus).NotNull();
        RuleFor(x => x.CreatedAt).NotEmpty().Must(x => x != default);
        RuleFor(x => x.Description).MaximumLength(500);
        RuleFor(x => x.AccountId).NotEmpty().Must(accountId => accountId != default);
        RuleFor(x => x.Items).NotEmpty().Must(items => items.Any());
        RuleForEach(x => x.Items).SetValidator(new FinancialTransactionItemRequestValidator());
    }
}

public class EntryFinancialTransactionRequestValidator : AbstractValidator<EntryFinancialTransactionRequest>
{
    public EntryFinancialTransactionRequestValidator()
    {
        RuleFor(x => x).SetValidator(new BaseFinancialTransactionRequestValidator());
        RuleFor(x => x.EntryId).Must(entryId => entryId.HasValue && entryId.Value != default);
    }
}

public class AccountFinancialTransactionRequestValidator : AbstractValidator<AccountFinancialTransactionRequest>
{
    public AccountFinancialTransactionRequestValidator()
    {
        RuleFor(x => x.SecondAccountId)
            .NotEqual(x => x.AccountId).WithMessage("Аккаунты должны быть различны")
            .Must(account2Id => account2Id.HasValue && account2Id.Value != default);
        RuleFor(x => x).SetValidator(new BaseFinancialTransactionRequestValidator());
    }
}
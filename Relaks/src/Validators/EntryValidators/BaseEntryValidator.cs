using FluentValidation;
using Relaks.Models;

namespace Relaks.Validators.EntryValidators;

public class BaseEntryValidator : AbstractValidator<BaseEntry>
{
    public BaseEntryValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MinimumLength(1).MaximumLength(150);
        When(x => !string.IsNullOrEmpty(x.Description), () =>
        {
            RuleFor(x => x.Description).MinimumLength(1).MaximumLength(300);
        });
        RuleFor(x => x.Discriminator).NotEmpty().Must(IsValidDiscriminator);
        RuleFor(x => x).SetValidator(new SoftDeletedValidator());
    }

    private bool IsValidDiscriminator(string disc)
    {
        return new List<string>() {nameof(EPerson), nameof(EProject), nameof(ECompany)}.Contains(disc);
    }
}

// public class EPersonValidator : AbstractValidator<EPerson>
// {
//     public EPersonValidator()
//     {
//         RuleFor(x => x).SetValidator(new BaseEntryValidator());
//     }
// }
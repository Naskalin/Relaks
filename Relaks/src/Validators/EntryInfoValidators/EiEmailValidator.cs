using FluentValidation;
using Relaks.Models;

namespace Relaks.Validators.EntryInfoValidators;

public class EiEmailValidator : AbstractValidator<EiEmail>
{
    public EiEmailValidator()
    {
        RuleFor(x => x).SetValidator(new EntryInfoValidator());
        RuleFor(x => x).SetValidator(new SoftDeletedValidator());
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
    }
}
using FluentValidation;
using Relaks.Models;

namespace Relaks.Validators.EntryInfoValidators;

public class EiPhoneValidator : AbstractValidator<EiPhone>
{
    public EiPhoneValidator()
    {
        RuleFor(x => x).SetValidator(new EntryInfoValidator());
        RuleFor(x => x).SetValidator(new SoftDeletedValidator());
        RuleFor(x => x).SetValidator(new PhoneValidator());
    }
}
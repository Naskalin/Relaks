using FluentValidation;
using Relaks.Models;

namespace Relaks.Validators.EntryInfoValidators;

public class EiDateValidator : AbstractValidator<EiDate>
{
    public EiDateValidator()
    {
        RuleFor(x => x).SetValidator(new EntryInfoValidator());
        RuleFor(x => x).SetValidator(new SoftDeletedValidator());
        RuleFor(x => x.Date).NotEmpty().NotEqual(default(DateTime));
    }
}
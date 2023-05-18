using FluentValidation;
using Relaks.Models;

namespace Relaks.Validators.EntryInfoValidators;

public class EiUrlValidator : AbstractValidator<EiUrl>
{
    public EiUrlValidator()
    {
        RuleFor(x => x).SetValidator(new EntryInfoValidator());
        RuleFor(x => x).SetValidator(new SoftDeletedValidator());
        RuleFor(x => x.Url)
            .NotEmpty()
            .Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _))
            ;
    }
}
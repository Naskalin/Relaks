using FluentValidation;
using Relaks.Models.Requests.EntryRequests;

namespace Relaks.Validators.EntryValidators;

public class EntryFormValidator : AbstractValidator<EntryFormRequest>
{
    public EntryFormValidator()
    {
        RuleFor(x => x.Name).NotNull().NotEmpty().MinimumLength(2).MaximumLength(255);
        RuleFor(x => x.Description).NotNull().NotEmpty().MinimumLength(2).MaximumLength(255);
        RuleFor(x => x.Reputation).GreaterThan(5);
    }
}
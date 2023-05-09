using FluentValidation;
using Relaks.Models;
using Relaks.Models.Requests.EntryRequests;
using Relaks.Pages.Entries;

namespace Relaks.Validators.EntryValidators;

public class EntryFormRequestValidator : AbstractValidator<EntryFormRequest>
{
    public EntryFormRequestValidator()
    {
        RuleFor(x => x.Name).NotNull().NotEmpty().MinimumLength(2).MaximumLength(150);
        RuleFor(x => x.Description).MinimumLength(2).MaximumLength(300);
        // RuleFor(x => x.Reputation).GreaterThan(5);
        RuleFor(x => x.Discriminator).NotEmpty().Must(IsValidDiscriminator);
    }

    private bool IsValidDiscriminator(string disc)
    {
        return new List<string>() {nameof(EPerson), nameof(EMeet), nameof(ECompany)}.Contains(disc);
    }
}
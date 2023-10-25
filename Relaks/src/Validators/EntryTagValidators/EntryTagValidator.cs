using FluentValidation;
using Relaks.Models;

namespace Relaks.Validators.EntryTagValidators;

public class EntryTagValidator : AbstractValidator<EntryTag>
{
    public EntryTagValidator()
    {
        RuleFor(x => x.EntryId).NotEmpty().Must(entryId => entryId != default);
        RuleFor(x => x.TagId).NotEmpty().Must(entryId => entryId != default);
        RuleFor(x => x.Description).MaximumLength(255);
    }
}
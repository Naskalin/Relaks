using FluentValidation;
using Relaks.Models;

namespace Relaks.Validators.EntryTagValidators;

public class EntryTagTitleValidator : AbstractValidator<EntryTagTitle>
{
    public EntryTagTitleValidator()
    {
        RuleFor(x => x.Title).NotEmpty().Length(1, 255);
        RuleFor(x => x.CategoryId).NotEmpty().Must(categoryId => categoryId != default);
    }
}
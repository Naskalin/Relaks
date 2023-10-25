using FluentValidation;
using Relaks.Database;
using Relaks.Models;

namespace Relaks.Validators.EntryTagValidators;

public class EntryTagCategoryValidator : AbstractValidator<EntryTagCategory>
{
    public EntryTagCategoryValidator(AppDbContext db)
    {
        RuleFor(x => x.Title).NotEmpty().Length(1, 150);
        RuleFor(x => x).SetValidator(new TreeValidator<EntryTagCategory>(db));
    }
}
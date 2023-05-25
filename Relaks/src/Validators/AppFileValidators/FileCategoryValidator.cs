using FluentValidation;
using Relaks.Database;
using Relaks.Models;

namespace Relaks.Validators.AppFileValidators;

public class BaseFileCategoryValidator : AbstractValidator<BaseFileCategory>
{
    public BaseFileCategoryValidator(AppDbContext db)
    {
        RuleFor(x => x.Title).NotEmpty();
        RuleFor(x => x).SetValidator(new TagValidator());
        RuleFor(x => x).SetValidator(new TreeValidator<BaseFileCategory>(db));
    }
}

public class EntryFileCategoryValidator : AbstractValidator<EntryFileCategory>
{
    public EntryFileCategoryValidator(AppDbContext db)
    {
        RuleFor(x => x).SetValidator(new BaseFileCategoryValidator(db));
        RuleFor(x => x.EntryId).NotEmpty();
    }
}
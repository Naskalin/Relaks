using FluentValidation;
using Relaks.Models;

namespace Relaks.Validators.AppFileValidators;

public class BaseFileCategoryValidator : AbstractValidator<BaseFileCategory>
{
    public BaseFileCategoryValidator()
    {
        RuleFor(x => x.Title).NotEmpty();
        RuleFor(x => x).SetValidator(new TagValidator());
    }
}

public class EntryFileCategoryValidator : AbstractValidator<EntryFileCategory>
{
    public EntryFileCategoryValidator()
    {
        RuleFor(x => x).SetValidator(new BaseFileCategoryValidator());
        RuleFor(x => x.EntryId).NotEmpty();
    }
}
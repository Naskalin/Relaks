using FluentValidation;
using Relaks.Models;

namespace Relaks.Validators.AppFileValidators;

public class BaseFileTagValidator : AbstractValidator<BaseFileTag>
{
    public BaseFileTagValidator()
    {
        RuleFor(x => x.Title).NotEmpty();
        RuleFor(x => x).SetValidator(new TagValidator());
    }
}

public class EntryFileTagValidator : AbstractValidator<EntryFileTag>
{
    public EntryFileTagValidator()
    {
        RuleFor(x => x).SetValidator(new BaseFileTagValidator());
    }
}
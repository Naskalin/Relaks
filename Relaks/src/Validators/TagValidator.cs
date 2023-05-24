using FluentValidation;
using Relaks.Interfaces;

namespace Relaks.Validators;

public class TagValidator : AbstractValidator<ITag>
{
    public TagValidator()
    {
        RuleFor(x => x.Title).Length(1, 150);
    }
}
using FluentValidation;
using Relaks.Interfaces;
using Relaks.Models.Store;

namespace Relaks.Validators;

public class EntryRelationValidator : AbstractValidator<EntryRelationRequest>
{
    public EntryRelationValidator()
    {
        RuleFor(x => x.FirstId).NotEmpty().NotEqual(default(Guid));
        RuleFor(x => x.SecondId).NotEmpty().NotEqual(default(Guid));
        RuleFor(x => x).Must(x => !x.FirstId.Equals(x.SecondId)).WithMessage("Объединение не может быть связано само с собой");
        RuleFor(x => x.FirstRating).InclusiveBetween(1, 10);
        RuleFor(x => x.SecondRating).InclusiveBetween(1, 10);
        RuleFor(x => x.FirstDescription).MaximumLength(1500);
        RuleFor(x => x.SecondDescription).MaximumLength(1500);
    }
}
using FluentValidation;
using Relaks.Database;
using Relaks.Interfaces;
using Relaks.Models.Store;

namespace Relaks.Validators;

public class EntryRelationValidator : AbstractValidator<EntryRelationRequest>
{
    private readonly AppDbContext _db;

    public EntryRelationValidator(AppDbContext db)
    {
        _db = db;
        RuleFor(x => x.FirstId).NotEmpty().NotEqual(default(Guid));
        RuleFor(x => x.SecondId).NotEmpty().NotEqual(default(Guid));
        RuleFor(x => x.SecondId).Must((x, _) => !x.SecondId.Equals(x.FirstId)).WithMessage("Объединение не может быть связано само с собой");
        RuleFor(x => x.FirstRating).InclusiveBetween(1, 10);
        RuleFor(x => x.SecondRating).InclusiveBetween(1, 10);
        RuleFor(x => x.Description).MaximumLength(3000);

        When(x => x.FirstId.HasValue && x.SecondId.HasValue, () =>
        {
            RuleFor(x => x).Must(x => !IsDuplicate(x.FirstId!.Value, x.SecondId!.Value)).WithMessage("Ошибка дублирования. Такое взаимоотношение уже присутствует.");
        });
    }

    private bool IsDuplicate(Guid firstId, Guid secondId)
    {
        return _db.EntryRelations.Any(x => 
            x.FirstId.Equals(firstId) && x.SecondId.Equals(secondId)
            || x.FirstId.Equals(secondId) && x.SecondId.Equals(firstId)
        );
    }
}
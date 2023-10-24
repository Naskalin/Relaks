using BootstrapBlazor.Components;
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
            RuleFor(x => x).Must(x => !IsDuplicate(x)).WithMessage("Ошибка дублирования. Такое взаимоотношение уже присутствует.");
        });
    }

    private bool IsDuplicate(EntryRelationRequest req)
    {
        return _db.EntryRelations
            .Where(x => !x.Id.Equals(req.Id))
            .Any(x => 
            x.FirstId.Equals(req.FirstId) && x.SecondId.Equals(req.SecondId)
            || x.FirstId.Equals(req.SecondId) && x.SecondId.Equals(req.FirstId)
        );
    }
}
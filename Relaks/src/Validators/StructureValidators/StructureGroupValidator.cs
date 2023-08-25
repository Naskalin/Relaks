using FluentValidation;
using Relaks.Database;
using Relaks.Models.StructureModels;

namespace Relaks.Validators.StructureValidators;

public class StructureGroupValidator : AbstractValidator<StructureGroup>
{
    public StructureGroupValidator(AppDbContext db)
    {
        RuleFor(x => x.Title).NotEmpty().MaximumLength(150);
        RuleFor(x => x.Description).MaximumLength(300);
        RuleFor(x => x.EndAt).Must((group, _) =>
        {
            if (group.EndAt != null)
            {
                return group.EndAt >= group.StartAt;
            }

            return true;
        }).WithMessage("Дата окончания должна быть более или равна дате начала");
        
        RuleFor(x => x).SetValidator(new TreeValidator<StructureGroup>(db));
    }
}
using FluentValidation;
using Relaks.Models.StructureModels;

namespace Relaks.Validators.StructureValidators;

public class StructureGroupValidator : AbstractValidator<StructureGroup>
{
    public StructureGroupValidator()
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
    }
}
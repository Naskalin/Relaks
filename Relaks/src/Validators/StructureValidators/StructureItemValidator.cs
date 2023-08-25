using FluentValidation;
using Relaks.Models.StructureModels;

namespace Relaks.Validators.StructureValidators;

public class StructureItemValidator : AbstractValidator<StructureItem>
{
    public StructureItemValidator()
    {
        RuleFor(x => x.Title).MaximumLength(150);
        RuleFor(x => x.Description).MaximumLength(300);
        RuleFor(x => x.EntryId).NotEmpty();
        RuleFor(x => x.GroupId).NotEmpty();
        RuleFor(x => x.EndAt).Must((item, _) =>
        {
            if (item.EndAt != null)
            {
                return item.EndAt >= item.StartAt;
            }

            return true;
        }).WithMessage("Дата окончания должна быть более или равна дате начала");
    }
}
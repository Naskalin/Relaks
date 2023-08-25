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
    }
}
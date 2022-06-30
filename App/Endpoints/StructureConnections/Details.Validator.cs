using App.Models;
using FluentValidation;

namespace App.Endpoints.StructureConnections;

public class DetailsValidator : AbstractValidator<StructureConnectionFormDetails>
{
    public DetailsValidator()
    {
        RuleFor(x => x.Description).NotNull().Length(0, 500);
        RuleFor(x => x.StructureFirstId).NotEmpty();
        RuleFor(x => x.StructureSecondId).NotEmpty();
        RuleFor(x => x.StructureFirstId)
            .Must((model, field) => !field.Equals(model.StructureSecondId))
            .WithMessage("Группа может быть связана только с другой группой.");
        RuleFor(x => x.StartAt).NotEqual(default(DateTime));
        RuleFor(x => x.DeletedAt).NotEqual(default(DateTime));
        RuleFor(x => x.DeletedReason).NotNull().Length(0, 250);
    }
}
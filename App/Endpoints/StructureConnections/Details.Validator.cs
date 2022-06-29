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
        RuleFor(x => x.StructureFirstId).Must((model, field) => !field.Equals(model.StructureSecondId));
        RuleFor(x => x.Direction).NotEmpty();
    }
}
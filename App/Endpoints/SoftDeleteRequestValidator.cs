using Bogus;
using FluentValidation;

namespace App.Endpoints;

public class SoftDeleteRequestValidator : AbstractValidator<ISoftDeleteRequest>
{
    public SoftDeleteRequestValidator()
    {
        RuleFor(x => x.DeletedReason).NotNull().Length(0, 250);
    }
}
using FluentValidation;

namespace App.Endpoints.Entries.EntryStructures;

public class DetailsValidator : AbstractValidator<CreateStructureRequestDetails>
{
    public DetailsValidator()
    {
        RuleFor(x => x.Title).NotEmpty().Length(1, 150);
        RuleFor(x => x.Description).NotNull().Length(0, 250);
        RuleFor(x => x.StartAt).NotEqual(default(DateTime));

        RuleFor(x => x.DeletedAt).NotEqual(default(DateTime));
        RuleFor(x => x.DeletedReason).NotNull().Length(0, 250);
    }
}
using App.Models;
using FluentValidation;

namespace App.Endpoints.Entries.Dates;

public class CreateRequestValidator : AbstractValidator<EntryDateDetails>
{
    public CreateRequestValidator()
    {
        RuleFor(x => x.Title).NotNull().Length(2, 255);
        RuleFor(x => x.Val).NotEmpty().NotEqual(default(DateTime));

        RuleFor(x => x.ActualStartAt).NotEmpty().NotEqual(default(DateTime));
        RuleFor(x => x.ActualEndAt).NotEqual(default(DateTime));
        RuleFor(x => x.ActualStartAtReason).NotNull().Length(0, 500);
        RuleFor(x => x.ActualEndAtReason).NotNull().Length(0, 500);
    }
}
using App.Models;
using FluentValidation;

namespace App.Endpoints.Entries.Texts;

public class CreateRequestValidator : AbstractValidator<CreateRequestDetails>
{
    public CreateRequestValidator()
    {
        RuleFor(x => x.About).NotNull().Length(0, 255);
        RuleFor(x => x.Val).NotEmpty();
        RuleFor(x => x.TextType).IsEnumName(typeof(EntryTextTypeEnum), false);

        RuleFor(x => x.ActualStartAt).NotEmpty().NotEqual(default(DateTime));
        RuleFor(x => x.ActualEndAt).NotEqual(default(DateTime));
        RuleFor(x => x.ActualStartAtReason).NotNull().Length(0, 500);
        RuleFor(x => x.ActualEndAtReason).NotNull().Length(0, 500);
    }
}
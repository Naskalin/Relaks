using App.Models;
using FluentValidation;

namespace App.Endpoints.Entries;

public class CreateRequestValidator : AbstractValidator<CreateRequest>
{
    public CreateRequestValidator()
    {
        RuleFor(x => x.Name).NotNull().Length(2, 150);
        RuleFor(x => x.EntryType).IsEnumName(typeof(EntryTypeEnum), caseSensitive: false);
        RuleFor(x => x.Description).NotNull().Length(0, 255);
        RuleFor(x => x.Reputation).InclusiveBetween(0, 10);
        RuleFor(x => x.StartAt).NotEqual(default(DateTime));
        RuleFor(x => x.EndAt).NotEqual(default(DateTime));
        
        RuleFor(x => x.ActualStartAt).NotEmpty().NotEqual(default(DateTime));
        RuleFor(x => x.ActualEndAt).NotEqual(default(DateTime));
        RuleFor(x => x.ActualStartAtReason).NotNull().Length(0, 500);
        RuleFor(x => x.ActualEndAtReason).NotNull().Length(0, 500);
    }
}
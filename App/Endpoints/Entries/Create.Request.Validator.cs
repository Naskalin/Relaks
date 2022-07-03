using App.Models;
using FluentValidation;

namespace App.Endpoints.Entries;

public class CreateRequestValidator : AbstractValidator<CreateRequest>
{
    public CreateRequestValidator()
    {
        RuleFor(x => x.Name).NotEmpty().Length(2, 250);
        RuleFor(x => x.EntryType).IsEnumName(typeof(EntryTypeEnum), false);
        RuleFor(x => x.Description).NotNull().Length(0, 500);
        RuleFor(x => x.Reputation).InclusiveBetween(0, 10);
        RuleFor(x => x.StartAt).NotEqual(default(DateTime));
        RuleFor(x => x.EndAt).NotEqual(default(DateTime));
        
        RuleFor(x => x.DeletedAt).NotEqual(default(DateTime));
        RuleFor(x => x.DeletedReason).NotNull().Length(0, 250);
    }
}
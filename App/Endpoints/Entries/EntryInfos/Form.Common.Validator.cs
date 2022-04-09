using FluentValidation;

namespace App.Endpoints.Entries.EntryInfos;

public class FormCommonValidator : AbstractValidator<IEntryInfoFormCommonRequest>
{
    public FormCommonValidator()
    {
        RuleFor(x => x.Title).NotNull().Length(0, 255);
        RuleFor(x => x.DeletedAt).NotEqual(default(DateTime));
        RuleFor(x => x.DeletedReason).NotNull().Length(0, 250);
    }
}
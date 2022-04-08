using FluentValidation;

namespace App.Endpoints.Entries.EntryInfos;

public class FormCommonValidator : AbstractValidator<IEntryInfoFormCommonRequest>
{
    public FormCommonValidator()
    {
        RuleFor(x => x.Title).NotNull().Length(0, 255);
    }
}
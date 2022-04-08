using FluentValidation;

namespace App.Endpoints.Entries.EntryInfos.Email;

public class FormRequestValidator : AbstractValidator<EntryEmailCreateRequestDetails>
{
    public FormRequestValidator()
    {
        Include(new FormCommonValidator());
        RuleFor(x => x.Email).EmailAddress();
    }
}
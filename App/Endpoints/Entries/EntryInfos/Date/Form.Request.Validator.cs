using FluentValidation;

namespace App.Endpoints.Entries.EntryInfos.Date;

public class FormRequestValidator : AbstractValidator<RequestDateDetails>
{
    public FormRequestValidator()
    {
        Include(new FormCommonValidator());
        RuleFor(x => x.Date).NotEmpty().NotEqual(default(DateTime));
    }
}
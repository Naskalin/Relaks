using FluentValidation;

namespace App.Endpoints.Entries.EntryInfos.Date;

public class CreateRequestValidator : AbstractValidator<EntryDateCreateRequestDetails>
{
    public CreateRequestValidator()
    {
        Include(new FormCommonValidator());
        RuleFor(x => x.Date).NotEmpty().NotEqual(default(DateTime));
    }
}
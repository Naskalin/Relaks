using App.Models;
using App.Utils;
using FluentValidation;

namespace App.Endpoints.Entries.EntryInfos;

public class RequestDetailsValidator : AbstractValidator<EntryInfoRequestDetails>
{
    public RequestDetailsValidator()
    {
        RuleFor(x => x.Title).NotNull().Length(0, 255);
        RuleFor(x => x.DeletedAt).NotEqual(default(DateTime));
        RuleFor(x => x.DeletedReason).NotNull().Length(0, 250);

        When(x => x.Type == EntryInfoType.Email, () =>
        {
            RuleFor(x => x.Email()).NotEmpty();
            RuleFor(x => x.Email()!.Email).NotEmpty().EmailAddress();
        });
    }
}
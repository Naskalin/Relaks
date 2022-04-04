using FluentValidation;

namespace App.Endpoints.Entries.EntryInfos.Url;

public class FormRequestValidator : AbstractValidator<RequestUrlDetails>
{
    public FormRequestValidator()
    {
        Include(new FormCommonValidator());
        RuleFor(x => x.Url)
            .Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _))
            .WithMessage("Требуется абсолютный url.");
    }
}
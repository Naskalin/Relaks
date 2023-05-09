using FluentValidation;
using Relaks.Models.Requests.EntryInfoRequests;

namespace Relaks.Validators.EntryInfoValidators;

public class EntryInfoCreateRequestValidator : AbstractValidator<EntryInfoCreateRequest>
{
    public EntryInfoCreateRequestValidator()
    {
        RuleFor(x => x).SetValidator(new EntryInfoFormRequestValidator());
    }
}
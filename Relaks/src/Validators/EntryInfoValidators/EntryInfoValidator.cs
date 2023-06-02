using FluentValidation;
using Relaks.Interfaces;

namespace Relaks.Validators.EntryInfoValidators;

public class EntryInfoValidator : AbstractValidator<IEntryInfo>
{
    public EntryInfoValidator()
    {
        When(x => !string.IsNullOrEmpty(x.Title), () =>
        {
            RuleFor(x => x.Title).Length(1, 255);
        });
    }
    
  
}
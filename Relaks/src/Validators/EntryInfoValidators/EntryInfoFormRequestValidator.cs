using FluentValidation;
using Relaks.Models;
using Relaks.Models.Requests.EntryInfoRequests;
using Relaks.Utils;

namespace Relaks.Validators.EntryInfoValidators;

public class EntryInfoFormRequestValidator : AbstractValidator<EntryInfoFormRequest>
{
    public EntryInfoFormRequestValidator()
    {
        RuleFor(x => x.Title).MinimumLength(2).MaximumLength(255);
        
        When(x => x.Discriminator.Equals(nameof(EiDate)), () =>
        {
            RuleFor(x => x.Date).NotEmpty().NotEqual(default(DateTime));
        });

        When(x => x.Discriminator.Equals(nameof(EiUrl)), () =>
        {
            RuleFor(x => x.Url)
                .NotEmpty()
                .Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _))
                .WithMessage("Абсолютная ссылка, например: https://ya.ru")
                ;
        });
        
        When(x => x.Discriminator.Equals(nameof(EiEmail)), () =>
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
        });
        
        When(x => x.Discriminator.Equals(nameof(EiPhone)), () =>
        {
            RuleFor(x => x.Number).NotEmpty();
            RuleFor(x => x.Region).NotEmpty().Length(2, 2);
            RuleFor(x => x).NotEmpty().Must(eiPhone => IsPhoneValid(eiPhone.Number, eiPhone.Region))
                .WithMessage(x => $"Номер телефона {x.Number} не может существовать для региона {x.Region}")
                ;
        });
    }
    
    private bool IsPhoneValid(string? number, string? region)
    {
        if (string.IsNullOrEmpty(number) || string.IsNullOrEmpty(region)) return false;
        
        try
        {
            PhoneHelper.ToPhone(number, region);
            return true;
        }
        catch
        {
            return false;
        }
    }
}
using FluentValidation;
using Relaks.Interfaces;
using Relaks.Utils;

namespace Relaks.Validators;

public class PhoneValidator : AbstractValidator<IPhone>
{
    public PhoneValidator()
    {
        RuleFor(x => x.Number).NotEmpty();
        RuleFor(x => x.Region).NotEmpty().Length(2, 2);
        RuleFor(x => x).NotEmpty().Must(eiPhone => IsPhoneValid(eiPhone.Number, eiPhone.Region))
            .WithMessage(x => $"Номер телефона {x.Number} не может существовать для региона {x.Region}")
            ;
    }
    
    private static bool IsPhoneValid(string? number, string? region)
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
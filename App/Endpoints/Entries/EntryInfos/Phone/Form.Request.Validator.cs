using App.Utils;
using FluentValidation;

namespace App.Endpoints.Entries.EntryInfos.Phone;

public class FormRequestValidator : AbstractValidator<RequestPhoneDetails>
{
    public FormRequestValidator()
    {
        Include(new FormCommonValidator());
        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .Must((x, phoneNumber) => IsPhoneValid(x.PhoneNumber, x.PhoneRegion))
            .WithMessage("Данный номер телефона не может существовать для выбранного региона.");
        RuleFor(x => x.PhoneRegion).NotEmpty().Length(2, 2);
    }

    private bool IsPhoneValid(string number, string region)
    {
        try
        {
            PhoneHelper.ToPhone(number, region);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}
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

        When(x => x.Type == EntryInfoType.Phone, () =>
        {
            RuleFor(x => x.PhoneUnformatted()).NotEmpty();
            RuleFor(x => x.PhoneUnformatted()!.Number).NotEmpty();
            RuleFor(x => x.PhoneUnformatted()!.Region).NotEmpty().Length(2, 2);
            RuleFor(x => x.PhoneUnformatted()!)
                .Must((x, phone) => IsPhoneValid(phone.Number, phone.Region))
                .WithMessage("Данный номер телефона не может существовать для выбранного региона.")
                ;
        });
        
        When(x => x.Type == EntryInfoType.Note, () =>
        {
            RuleFor(x => x.Note()).NotEmpty();
            RuleFor(x => x.Note()!.Note).NotEmpty().MaximumLength(10000);
        });
        
        When(x => x.Type == EntryInfoType.Date, () =>
        {
            RuleFor(x => x.Date()).NotEmpty();
            RuleFor(x => x.Date()!.Date).NotEmpty().NotEqual(default(DateTime));
        });
        
        When(x => x.Type == EntryInfoType.Url, () =>
        {
            RuleFor(x => x.Url()).NotEmpty();
            RuleFor(x => x.Url()!.Url)
                .NotEmpty()
                .Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _))
                .WithMessage("Здесь нужен абсолютный (полный) url.")
                ;
        });
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
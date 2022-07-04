using App.Models;
using App.Utils;
using FluentValidation;

namespace App.Endpoints.Entries.EntryInfos;

public class RequestDetailsValidator : AbstractValidator<EntryInfoRequestDetails>
{
    public RequestDetailsValidator()
    {
        RuleFor(x => x.Title).NotNull().Length(0, 250);
        RuleFor(x => x.DeletedAt).NotEqual(default(DateTime));
        RuleFor(x => x.DeletedReason).NotNull().Length(0, 250);
        RuleFor(x => x.Info).NotEmpty();

        When(x => EntryInfo.Email.Equals(x.Type), () =>
        {
            RuleFor(x => x.Email()).NotEmpty();
            RuleFor(x => x.Email()!.Email).NotEmpty().EmailAddress();
        });

        When(x => EntryInfo.Phone.Equals(x.Type), () =>
        {
            RuleFor(x => x.PhoneUnformatted()).NotEmpty();
            RuleFor(x => x.PhoneUnformatted()!.Number).NotEmpty();
            RuleFor(x => x.PhoneUnformatted()!.Region).NotEmpty().Length(2, 2);
            RuleFor(x => x.PhoneUnformatted()!)
                .Must((_, phone) => IsPhoneValid(phone.Number, phone.Region))
                .WithMessage("Данный номер телефона не может существовать для выбранного региона.")
                ;
        });
        
        When(x => EntryInfo.Note.Equals(x.Type), () =>
        {
            RuleFor(x => x.Note()).NotEmpty();
            RuleFor(x => x.Note()!.Note).NotEmpty().MaximumLength(300);
        });
        
        When(x => EntryInfo.Date.Equals(x.Type), () =>
        {
            RuleFor(x => x.Date()).NotEmpty();
            RuleFor(x => x.Date()!.Date).NotEmpty().NotEqual(default(DateTime));
        });
        
        When(x => EntryInfo.Url.Equals(x.Type), () =>
        {
            RuleFor(x => x.Url()).NotEmpty();
            RuleFor(x => x.Url()!.Url)
                .NotEmpty()
                .Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _))
                .WithMessage("Здесь нужен абсолютный url, например: https://ya.ru")
                ;
        });
        
        When(x => EntryInfo.Custom.Equals(x.Type), () =>
        {
            RuleFor(x => x.Custom()).NotEmpty();
            RuleForEach(x => x.Custom()!.Groups).NotEmpty();
            RuleForEach(x => x.Custom()!.Groups)
                .ChildRules(groups =>
                {
                    groups.RuleFor(x => x.Title).NotNull().Length(0, 250);
                    groups.RuleForEach(x => x.Items).NotEmpty();
                    groups.RuleForEach(x => x.Items).ChildRules(item =>
                    {
                        item.RuleFor(x => x.Key).NotNull().Length(0, 250);
                        item.RuleFor(x => x.Value).NotNull().Length(0, 1000);
                        item.RuleFor(x => x).Must((_, row) => IsOneNotEmpty(row))
                            .WithMessage("Вы не можете сохранить пустую строку, ключ или значение должны быть заполнены.");
                    });
                });
        });
    }

    // Хотя бы один не пустой, иначе будут просто пустые строки
    private bool IsOneNotEmpty(CustomInfoItem item)
    {
        return !String.IsNullOrEmpty(item.Key) || !String.IsNullOrEmpty(item.Value);
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
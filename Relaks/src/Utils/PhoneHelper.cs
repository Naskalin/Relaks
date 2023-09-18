using Relaks.Models;

using PhoneNumbers;
using Relaks.Interfaces;

namespace Relaks.Utils;

public class MyPhone : IPhone
{
    public string Number { get; set; } = null!;
    public string Region { get; set; } = null!;

    public MyPhone()
    {
    }

    public MyPhone(string number, string region)
    {
        Number = number;
        Region = region;
    }
}

public static class PhoneHelper
{
    public static string? PhoneFormat(this IPhone phone, PhoneNumberFormat numberFormat)
    {
        var phoneNumberUtil = PhoneNumberUtil.GetInstance();
        var phoneNumber = phoneNumberUtil.Parse(phone.Number, phone.Region.ToUpper());
        return !phoneNumberUtil.IsValidNumber(phoneNumber) 
            ? null 
            : phoneNumberUtil.Format(phoneNumber, numberFormat);
    }
    
    public static MyPhone ToPhone(IPhone phone)
    {
        return ToPhone(phone.Number, phone.Region);
    }
    
    public static MyPhone ToPhone(string number, string region)
    {
        var phoneNumberUtil = PhoneNumberUtil.GetInstance();
        try
        {
            var phone = new MyPhone()
            {
                Region = region.ToUpper()
            };
            var phoneNumber = phoneNumberUtil.Parse(number, phone.Region);
            if (!phoneNumberUtil.IsValidNumber(phoneNumber))
            {
                throw new ArgumentException("Номер телефона не распознан");
            }

            phone.Number = phoneNumberUtil.Format(phoneNumber, PhoneNumberFormat.E164);
            return phone;
        }
        catch (Exception)
        {
            throw new ArgumentException(
                $"Номер телефона {number} не может существовать для {region} региона.");
        }
    }
}
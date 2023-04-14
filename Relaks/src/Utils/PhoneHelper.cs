using Relaks.Models;

using PhoneNumbers;

namespace Relaks.Utils;

public static class PhoneHelper
{
    public static PhoneInfo ToPhone(string number, string region)
    {
        var phoneNumberUtil = PhoneNumberUtil.GetInstance();
        try
        {
            var phone = new PhoneInfo
            {
                Region = region.ToUpper()
            };
            var phoneNumber = phoneNumberUtil.Parse(number, phone.Region);
            if (!phoneNumberUtil.IsValidNumber(phoneNumber))
            {
                throw new ArgumentException();
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
    
    // Предполагается, что это будет строка вида RU|1234567890
    public static PhoneInfo ToPhone(string regionWithNumber)
    {
        var phoneArr = regionWithNumber.Split("|");
        var phoneNumberUtil = PhoneNumberUtil.GetInstance();
        try
        {
            var phone = new PhoneInfo
            {
                Region = phoneArr[0].ToUpper()
            };
            var phoneNumber = phoneNumberUtil.Parse(phoneArr[1], phone.Region);
            if (!phoneNumberUtil.IsValidNumber(phoneNumber))
            {
                throw new ArgumentException();
            }

            phone.Number = phoneNumberUtil.Format(phoneNumber, PhoneNumberFormat.E164);
            return phone;
        }
        catch (Exception)
        {
            throw new ArgumentException(
                $"Номер телефона {regionWithNumber} не может существовать для выбранного региона.");
        }
    }
}
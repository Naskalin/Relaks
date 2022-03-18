using PhoneNumbers;

namespace App.Utils;

public class Phone
{
    public string E164 { get; set; } = null!;
    public string Region { get; set; } = null!;
}

public static class PhoneHelper
{
    // Предполагается, что это будет строка вида 1234567890|ru
    public static Phone ToPhone(string numberWithRegion)
    {
        var phoneArr = numberWithRegion.Split("|");
        var phoneNumberUtil = PhoneNumberUtil.GetInstance();
        try
        {
            var phone = new Phone();
            phone.Region = phoneArr[1].ToUpper();
            var phoneNumber = phoneNumberUtil.Parse(phoneArr[0], phone.Region);
            if (!phoneNumberUtil.IsValidNumber(phoneNumber))
            {
                throw new ArgumentException();
            }

            phone.E164 = phoneNumberUtil.Format(phoneNumber, PhoneNumberFormat.E164);
            return phone;
        }
        catch (Exception)
        {
            throw new ArgumentException(
                $"Ошибка при разборе {numberWithRegion}. Данный номер не может существовать для выбранного региона.");
        }
    }
}
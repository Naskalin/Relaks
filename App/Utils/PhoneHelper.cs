﻿using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PhoneNumbers;

namespace App.Utils;

public class Phone
{
    public string Number { get; set; } = null!;
    public string Region { get; set; } = null!;

    public override string ToString()
    {
        return Region + "|" + Number;
    }
}

public static class PhoneHelper
{
    public static Phone ToPhone(string number, string region)
    {
        var phoneNumberUtil = PhoneNumberUtil.GetInstance();
        try
        {
            var phone = new Phone();
            phone.Region = region.ToUpper();
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
    
    // Предполагается, что это будет строка вида 1234567890|ru
    public static Phone ToPhone(string numberWithRegion)
    {
        var phoneArr = numberWithRegion.Split("|");
        var phoneNumberUtil = PhoneNumberUtil.GetInstance();
        try
        {
            var phone = new Phone();
            phone.Region = phoneArr[0].ToUpper();
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
                $"Номер телефона {numberWithRegion} не может существовать для выбранного региона.");
        }
    }
}
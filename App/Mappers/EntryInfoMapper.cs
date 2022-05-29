using System.Text.Json;
using App.Endpoints.Entries.EntryInfos;
// using App.Endpoints.Entries.EntryInfos.Date;
// using App.Endpoints.Entries.EntryInfos.Email;
// using App.Endpoints.Entries.EntryInfos.Note;
// using App.Endpoints.Entries.EntryInfos.Phone;
// using App.Endpoints.Entries.EntryInfos.Url;
using App.Models;
using App.Utils;

namespace App.Mappers;

public static class EntryInfoMapper
{
    // private static void MapToCommon(IEntryInfoFormCommonRequest formCommon, EntryInfo eInfo)
    // {
    //     eInfo.Title = formCommon.Title.Trim();
    //     formCommon.SoftDeleteMapTo(eInfo);
    // }

    public static void MapTo(this EntryInfoRequestDetails details, EntryInfo eInfo)
    {
        details.SoftDeleteMapTo(eInfo);
        eInfo.Title = details.Title.Trim();
        eInfo.UpdatedAt = DateTime.UtcNow;

        switch (details.Type)
        {
            case EntryInfoType.Email:
                var email = details.Email()!;
                email.Email = email.Email.Trim().ToLower();
                eInfo.Value = JsonSerializer.Serialize(email, InfoValue.WriteOptions);
                break;
            case EntryInfoType.Phone:
                eInfo.Value = JsonSerializer.Serialize(details.Phone()!, InfoValue.WriteOptions);
                break;
            case EntryInfoType.Url:
                var url = details.Url()!;
                url.Url = url.Url.Trim();
                eInfo.Value = JsonSerializer.Serialize(url, InfoValue.WriteOptions);
                break;
            case EntryInfoType.Date:
                eInfo.Value = JsonSerializer.Serialize(details.Date()!, InfoValue.WriteOptions);
                break;
            case EntryInfoType.Note:
                eInfo.Value = JsonSerializer.Serialize(details.Note()!, InfoValue.WriteOptions);
                break;
            default:
                throw new ArgumentException($"EntryInfo Mapper for type: {eInfo.Type} not found.");
        }
    }
    // public static void MapTo(this RequestEmailDetails details, EntryEmail eInfo)
    // {
    //     MapToCommon(details, eInfo);
    //     eInfo.Email = details.Email.Trim().ToLower();
    // }

    // public static void MapTo(this RequestUrlDetails details, EntryUrl eInfo)
    // {
    //     MapToCommon(details, eInfo);
    //     eInfo.Url = details.Url.Trim().ToLower();
    // }
    //
    // public static void MapTo(this RequestEmailDetails details, EntryEmail eInfo)
    // {
    //     MapToCommon(details, eInfo);
    //     eInfo.Email = details.Email.Trim().ToLower();
    // }
    //
    // public static void MapTo(this RequestNoteDetails details, EntryNote eInfo)
    // {
    //     MapToCommon(details, eInfo);
    //     eInfo.Note = details.Note.Trim();
    // }
    //
    // public static void MapTo(this RequestDateDetails details, EntryDate eInfo)
    // {
    //     MapToCommon(details, eInfo);
    //     eInfo.Date = details.Date;
    // }
    //
    // public static void MapTo(this RequestPhoneDetails details, EntryPhone eInfo)
    // {
    //     MapToCommon(details, eInfo);
    //     var phone = PhoneHelper.ToPhone(details.PhoneNumber, details.PhoneRegion);
    //     eInfo.PhoneNumber = phone.Number;
    //     eInfo.PhoneRegion = phone.Region;
    // }
}
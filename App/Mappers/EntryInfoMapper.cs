using App.Endpoints.Entries.EntryInfos;
using App.Endpoints.Entries.EntryInfos.Date;
using App.Endpoints.Entries.EntryInfos.Email;
using App.Endpoints.Entries.EntryInfos.Note;
using App.Models;
using App.Utils;

namespace App.Mappers;

public static class EntryInfoMapper
{
    private static void MapToCommon(IEntryInfoFormCommonRequest formCommon, EntryInfo eInfo)
    {
        // Enum.TryParse(details.TextType, true, out TextTypeEnum textTypeEnum);
        // eInfo.TextType = textTypeEnum;
        eInfo.Title = formCommon.Title.Trim();
        eInfo.DeletedAt = formCommon.DeletedAt;
        eInfo.DeletedReason = formCommon.DeletedReason.Trim();

        // var val = details.Val.Trim();
        //
        // switch (eInfo.TextType)
        // {
        //     case TextTypeEnum.Phone:
        //         val = PhoneHelper.ToPhone(val).ToString();
        //         break;
        //     case TextTypeEnum.Url:
        //     case TextTypeEnum.Email:
        //         val = val.ToLower();
        //         break;
        // }
        //
        // eInfo.Val = val;
    }
    //
    // public static void MapTo(this EntryInfoDetails details, EntryInfoUrl eInfo)
    // {
    //     MapToCommon(details, eInfo);
    //     eInfo.Url = details.Url!.Trim().ToLower();
    // }
    //
    public static void MapTo(this RequestEmailDetails details, EntryEmail eInfo)
    {
        MapToCommon(details, eInfo);
        eInfo.Email = details.Email.Trim().ToLower();
    }
    
    public static void MapTo(this RequestNoteDetails details, EntryNote eInfo)
    {
        MapToCommon(details, eInfo);
        eInfo.Note = details.Note.Trim();
    }


    public static void MapTo(this RequestDateDetails details, EntryDate eInfo)
    {
        MapToCommon(details, eInfo);
        eInfo.Date = details.Date;
    }

    // public static void MapTo(this EntryInfoDetails details, EntryInfoPhone eInfo)
    // {
    //     MapToCommon(details, eInfo);
    //     // eInfo.Phone = details.Details.Url!.ToLower();
    //     throw new NotImplementedException();
    // }
}
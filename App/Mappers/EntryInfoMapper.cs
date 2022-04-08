using App.Endpoints;
using App.Endpoints.Entries.EntryInfos;
using App.Endpoints.Entries.EntryInfos.Date;
using App.Endpoints.Entries.EntryInfos.Email;
using App.Endpoints.Entries.EntryInfos.Note;
using App.Endpoints.Entries.EntryInfos.Phone;
using App.Endpoints.Entries.EntryInfos.Url;
using App.Models;
using App.Utils;

namespace App.Mappers;

public static class EntryInfoMapper
{
    private static void MapToCommon(IEntryInfoFormCommonRequest formCommon, EntryInfo eInfo)
    {
        eInfo.Title = formCommon.Title.Trim();
    }

    public static void MapTo(this ISoftDeleteRequest req, ISoftDelete model)
    {
        if (req.IsFullDelete != true)
        {
            model.DeletedAt = DateTime.UtcNow;
            model.DeletedReason = req.DeletedReason;
        }
    }

    public static void MapTo(this RequestUrlDetails details, EntryUrl eInfo)
    {
        MapToCommon(details, eInfo);
        eInfo.Url = details.Url.Trim().ToLower();
    }

    public static void MapTo(this EntryEmailCreateRequestDetails details, EntryEmail eInfo)
    {
        MapToCommon(details, eInfo);
        eInfo.Email = details.Email.Trim().ToLower();
    }

    public static void MapTo(this RequestNoteDetails details, EntryNote eInfo)
    {
        MapToCommon(details, eInfo);
        eInfo.Note = details.Note.Trim();
    }


    public static void MapTo(this EntryDateCreateRequestDetails createDetails, EntryDate eInfo)
    {
        MapToCommon(createDetails, eInfo);
        eInfo.Date = createDetails.Date;
    }

    public static void MapTo(this EntryDatePutRequestDetails details, EntryDate eInfo)
    {
        EntryDateCreateRequestDetails createMapTo = details;
        ISoftDeleteRequest softDeleteMapTo = details;
        createMapTo.MapTo(eInfo);
        softDeleteMapTo.MapTo(eInfo);
    }

    public static void MapTo(this RequestPhoneDetails details, EntryPhone eInfo)
    {
        MapToCommon(details, eInfo);
        var phone = PhoneHelper.ToPhone(details.PhoneNumber, details.PhoneRegion);
        eInfo.PhoneNumber = phone.Number;
        eInfo.PhoneRegion = phone.Region;
    }
}
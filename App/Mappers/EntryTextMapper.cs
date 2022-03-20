using App.Endpoints.Entries.Texts;
using App.Models;
using App.Utils;

namespace App.Mappers;

public static class EntryTextMapper
{
    private static void MapToCreateDetails(EntryTextDetails req, EntryText entryText)
    {
        Enum.TryParse(req.TextType, true, out TextTypeEnum textTypeEnum);
        entryText.TextType = textTypeEnum;
        entryText.About = req.About.Trim();

        entryText.ActualStartAt = req.ActualStartAt;
        entryText.ActualStartAtReason = req.ActualStartAtReason.Trim();
        entryText.ActualEndAt = req.ActualEndAt;
        entryText.ActualEndAtReason = req.ActualEndAtReason.Trim();

        var val = req.Val.Trim();

        switch (entryText.TextType)
        {
            case TextTypeEnum.Phone:
                val = PhoneHelper.ToPhone(val).ToString();
                break;
            case TextTypeEnum.Url:
            case TextTypeEnum.Email:
                val = val.ToLower();
                break;
        }
        
        entryText.Val = val;
    }
    
    public static void MapTo(this CreateRequest req, EntryText entryText)
    {
        MapToCreateDetails(req.Details, entryText);
    }

    public static void MapTo(this PutRequest req, EntryText entryText)
    {
        MapToCreateDetails(req.Details, entryText);
    }
}
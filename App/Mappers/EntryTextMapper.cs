using App.Endpoints.Entries.Texts;
using App.Models;

namespace App.Mappers;

public static class EntryTextMapper
{
    private static void MapToCreateDetails(CreateRequestDetails req, EntryText entryText)
    {
        Enum.TryParse(req.TextType, true, out EntryTextTypeEnum textTypeEnum);
        entryText.TextType = textTypeEnum;
        entryText.About = req.About;
        entryText.Val = req.Val;
        
        entryText.ActualStartAt = req.ActualStartAt;
        entryText.ActualStartAtReason = req.ActualStartAtReason;
        entryText.ActualEndAt = req.ActualEndAt;
        entryText.ActualEndAtReason = req.ActualEndAtReason;
    }
    
    public static void MapTo(this CreateRequest req, EntryText entryText)
    {
        MapToCreateDetails(req.Details, entryText);
    }
}
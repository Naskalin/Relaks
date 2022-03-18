using App.Endpoints.Entries.Dates;
using App.Models;

namespace App.Mappers;

public static class EntryDateMapper
{
    private static void MapToCreateDetails(CreateRequestDetails req, EntryDate entryDate)
    {
        entryDate.About = req.About;
        entryDate.Val = req.Val;
        
        entryDate.ActualStartAt = req.ActualStartAt;
        entryDate.ActualStartAtReason = req.ActualStartAtReason;
        entryDate.ActualEndAt = req.ActualEndAt;
        entryDate.ActualEndAtReason = req.ActualEndAtReason;
    }
    public static void MapTo(this CreateRequest req, EntryDate entryDate)
    {
        MapToCreateDetails(req.Details, entryDate);
    }

    public static void MapTo(this PutRequest req, EntryDate entryDate)
    {
        MapToCreateDetails(req.Details, entryDate);
    }
}
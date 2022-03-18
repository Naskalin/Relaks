using App.Endpoints.Entries;
using App.Models;
using App.Utils;

namespace App.Mappers;

public static class EntryMapper
{
    public static void MapTo(this CreateRequest req, Entry entry)
    {

        Enum.TryParse(req.EntryType, true, out EntryTypeEnum entryTypeEnum);
        entry.EntryType = entryTypeEnum;
        entry.Name = req.Name;
        entry.Description = req.Description;
        entry.Reputation = req.Reputation;
        entry.StartAt = req.StartAt;
        entry.EndAt = req.EndAt;
        entry.ActualStartAt = req.ActualStartAt;
        entry.ActualStartAtReason = req.ActualStartAtReason;
        entry.ActualEndAt = req.ActualEndAt;
        entry.ActualEndAtReason = req.ActualEndAtReason;
    }

    public static void MapTo(this PutRequest req, Entry entry)
    {
        MapTo(req.Details, entry);
    }
}
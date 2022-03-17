using App.Endpoints.Entries;
using App.Models;
using App.Utils;

namespace App.Mappers;

public static class EntryMapper
{
    public static void MapTo(this CreateRequest createRequest, Entry entry)
    {

        Enum.TryParse(createRequest.EntryType, true, out EntryTypeEnum entryTypeEnum);
        entry.EntryType = entryTypeEnum;
        entry.Name = createRequest.Name;
        entry.Description = createRequest.Description;
        entry.Reputation = createRequest.Reputation;
        entry.StartAt = createRequest.StartAt;
        entry.EndAt = createRequest.EndAt;
        entry.ActualStartAt = createRequest.ActualStartAt;
        entry.ActualStartAtReason = createRequest.ActualStartAtReason;
        entry.ActualEndAt = createRequest.ActualEndAt;
        entry.ActualEndAtReason = createRequest.ActualEndAtReason;
    }

    public static void MapTo(this PutRequest req, Entry entry)
    {
        MapTo(req.Details, entry);
    }
}
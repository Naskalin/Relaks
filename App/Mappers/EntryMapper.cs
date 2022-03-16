using App.Endpoints.Entries;
using App.Models;

namespace App.Mappers;

public static class EntryMapper
{
    public static void MapTo(this CreateRequest createRequest, Entry entry)
    {
        entry.EntryType = createRequest.EntryType;
        entry.Name = createRequest.Name;
        entry.Description = createRequest.Description;
        entry.Reputation = createRequest.Reputation ?? 5;
        entry.StartAt = createRequest.StartAt;
        entry.EndAt = createRequest.EndAt;
        entry.ActualStartAt = createRequest.ActualStartAt ?? DateTime.UtcNow;
        entry.ActualStartAtReason = createRequest.ActualStartAtReason;
        entry.ActualEndAt = createRequest.ActualEndAt;
        entry.ActualEndAtReason = createRequest.ActualEndAtReason;
    }

    public static void MapTo(this PatchRequest patchRequest, Entry entry)
    {
        // entry.Name = patchRequest.Details.Name;
        // entry.Name = patchRequest.Name;
        if (patchRequest.Details.Name.HasValue) entry.Name = patchRequest.Details.Name.Value;
        if (patchRequest.Details.Description.HasValue) entry.Description = patchRequest.Details.Description.Value;
    }
}
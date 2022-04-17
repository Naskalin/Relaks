using App.Endpoints.Entries;
using App.Models;
using App.Repository;

namespace App.Mappers;

public static class EntryMapper
{
    public static void MapTo(this CreateRequest req, Entry entry)
    {
        Enum.TryParse(req.EntryType, true, out EntryTypeEnum entryTypeEnum);
        entry.EntryType = entryTypeEnum;
        entry.Name = req.Name.Trim();
        entry.Description = req.Description.Trim();
        entry.Reputation = req.Reputation;
        entry.StartAt = req.StartAt;
        entry.EndAt = req.EndAt;

        req.SoftDeleteMapTo(entry);
    }

    public static void MapTo(this PutRequest req, Entry entry)
    {
        MapTo(req.Details, entry);
    }



    // public static void MapTo(this Entry entry, EntryFts entryFts)
    // {
    //     entryFts.Id = entry.Id;
    //     entryFts.Name = entry.Name;
    //     entryFts.Description = entry.Description;
    //     entryFts.DeletedReason = entry.DeletedReason;
    // }
}
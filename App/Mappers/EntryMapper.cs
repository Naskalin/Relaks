using App.Endpoints.Entries;
using App.Models;

namespace App.Mappers;

public static class EntryMapper
{
    public static void MapTo(this EntryFormRequest req, Entry entry)
    {
        Enum.TryParse(req.EntryType, true, out EntryTypeEnum entryTypeEnum);
        entry.EntryType = entryTypeEnum;
        entry.Name = req.Name.Trim();
        entry.Description = req.Description.Trim();
        entry.Reputation = req.Reputation;
        entry.StartAt = req.StartAt;
        entry.EndAt = req.EndAt;
        entry.UpdatedAt = DateTime.UtcNow;
        entry.Avatar = req.Avatar;

        req.MapToSoftDelete(entry);
    }
}
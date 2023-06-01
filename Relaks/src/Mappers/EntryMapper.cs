using Relaks.Interfaces;
using Relaks.Models;

namespace Relaks.Mappers;

public static class EntryMapper
{
    public static string ToFtsBody(this BaseEntry baseEntry)
    {
        var arr = new List<string?>
        {
            baseEntry.Name,
            baseEntry.Description,
            baseEntry.DeletedReason
        };

        return string.Join(" ", arr.Where(x => !string.IsNullOrEmpty(x)));
    }

    public static void MapTo(this IEntry from, IEntry to)
    {
        to.Name = from.Name.Trim();
        to.Description = from.Description?.Trim();
        to.EndAt = from.EndAt;
        to.StartAt = from.StartAt;
        to.StartAtWithTime = from.StartAtWithTime;
        to.EndAtWithTime = from.EndAtWithTime;
        to.Thumbnail = from.Thumbnail;
    }

    // public static void MapTo(this EntryFormRequest req, BaseEntry baseEntry)
    // {
    //     req.ToEntry(baseEntry);
    // }

    // public static void MapTo(this BaseEntry baseEntry, EntryUpdateRequest req)
    // {
    //     baseEntry.ToEntry(req);
    //     baseEntry.MapSoftDeleted(req);
    // }
}
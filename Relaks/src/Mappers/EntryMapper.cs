using Relaks.Interfaces;
using Relaks.Models;
using Relaks.Models.Requests.EntryRequests;

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

    private static void ToEntry(this IEntry from, IEntry to)
    {
        to.Name = from.Name;
        to.Description = from.Description;
        to.EndAt = from.EndAt;
        to.StartAt = from.StartAt;
    }

    public static void MapTo(this EntryFormRequest req, BaseEntry baseEntry)
    {
        req.ToEntry(baseEntry);
    }

    public static void MapTo(this BaseEntry baseEntry, EntryUpdateRequest req)
    {
        baseEntry.ToEntry(req);
        baseEntry.MapSoftDeleted(req);
    }
}
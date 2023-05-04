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

    public static string ToFtsDeletedAt(this BaseEntry baseEntry) => baseEntry.DeletedAt != null ? "1" : "";
}
using Relaks.Models;

namespace Relaks.Mappers;

public static class EntryMapper
{
    public static string ToFtsBody(this Entry entry)
    {
        var arr = new List<string?>
        {
            entry.Name,
            entry.Description,
            entry.DeletedReason
        };

        return string.Join(" ", arr.Where(x => !string.IsNullOrEmpty(x)));
    }
}
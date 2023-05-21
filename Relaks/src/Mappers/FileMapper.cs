using Relaks.Models;

namespace Relaks.Mappers;

public static class FileMapper
{
    public static string ToFtsBody(this BaseFile baseFile)
    {
        var arr = new List<string?>
        {
            baseFile.DisplayName,
            baseFile.DeletedReason
        };

        return string.Join(" ", arr.Where(x => !string.IsNullOrEmpty(x)));
    }
}
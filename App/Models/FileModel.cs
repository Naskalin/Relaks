using System.Text.RegularExpressions;

namespace App.Models;

public class FileModel : BaseEntity, ITimestampResource, ISoftDelete
{
    public string Path { get; set; } = null!;
    public string ContentType { get; set; } = null!;
    public string Name { get; set; } = null!;

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }
    public string DeletedReason { get; set; } = null!;

    public bool IsImage()
    {
        var pattern = @"image\/";
        var m = Regex.Match(ContentType, pattern, RegexOptions.IgnoreCase);
        return m.Success;
    }
}

public class EntryFile : FileModel
{
    public Guid EntryId { get; set; }

    public string GetFileDir()
    {
        return System.IO.Path.Combine(nameof(Entry).ToLowerInvariant(), EntryId.ToString());
    }

    public string GetFilePath()
    {
        return System.IO.Path.Combine(GetFileDir(), Path);
    }
}

public class EntryInfoFile : FileModel
{
    public Guid EntryInfoId { get; set; }
}
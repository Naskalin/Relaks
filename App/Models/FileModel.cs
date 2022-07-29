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
    public string Discriminator { get; set; } = null!;
    // public string Category { get; set; } = null!;
    // public string TempCategory { get; set; } = null!;
    
    public FileCategory? Category { get; set; }
    public Guid? CategoryId { get; set; }
    
    public List<string> Tags { get; set; } = new();

    public bool IsImage()
    {
        var pattern = @"image\/";
        var match = Regex.Match(ContentType, pattern, RegexOptions.IgnoreCase);
        return match.Success;
    }
}

public class EntryFile : FileModel
{
    public Guid EntryId { get; set; }

    public string GetFileRelativeDir()
    {
        return System.IO.Path.Combine("entry", EntryId.ToString());
    }

    public string GetFileRelativePath()
    {
        return System.IO.Path.Combine(GetFileRelativeDir(), Path);
    }
}

public class EntryInfoFile : FileModel
{
    public Guid EntryInfoId { get; set; }
    
    public string GetFileRelativeDir()
    {
        return System.IO.Path.Combine("entry_info", EntryInfoId.ToString());
    }

    public string GetFileRelativePath()
    {
        return System.IO.Path.Combine(GetFileRelativeDir(), Path);
    }
}
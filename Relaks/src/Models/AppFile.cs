using System.ComponentModel.DataAnnotations.Schema;
using Relaks.Interfaces;

namespace Relaks.Models;

[Table("Files")]
public abstract class BaseFile : IAppFile, ISoftDeletedReason
{
    public Guid Id { get; set; }
    public string Extension { get; set; } = null!;
    public string MimeType { get; set; } = null!;
    public string DisplayName { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    public string? DeletedReason { get; set; }
    public string Discriminator { get; set; } = null!;
    public string? Keyword { get; set; }
    
    public Guid? CategoryId { get; set; }
    public BaseFileCategory? Category { get; set; }

    public List<BaseFileTag> Tags { get; set; } = new();

    public bool IsImage() => MimeType.StartsWith("image");

    protected BaseFile()
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.Now;
    }

    public string FileName() => Id + Extension;

    public string DirPath()
    {
        return Path.Combine(
            "relaks_store",
            "files",
            CreatedAt.Year.ToString(),
            CreatedAt.Month.ToString(),
            CreatedAt.Day.ToString()
        );
    }

    public string FilePath()
    {
        return Path.Combine(DirPath(), FileName());
    }
}

public class EntryFile : BaseFile
{
    public Guid EntryId { get; set; }
    public BaseEntry Entry { get; set; } = null!;
}

public class EntryInfoFile : EntryFile
{
    public Guid EntryInfoId { get; set; }
    public BaseEntryInfo EntryInfo { get; set; } = null!;
}

public class FtsFile : IFtsEntity
{
    public int RowId { get; set; }
    public double? Rank { get; set; }
    public string Match { get; set; } = null!;
    public string Snippet { get; set; } = null!;
    
    public Guid Id { get; set; }
    public string Body { get; set; } = null!;
    
    public string DeletedAt { get; set; } = null!;
    public string Discriminator { get; set; } = null!;
}
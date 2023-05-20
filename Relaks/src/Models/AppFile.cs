using Relaks.Interfaces;

namespace Relaks.Models;

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
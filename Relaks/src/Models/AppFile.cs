using System.ComponentModel.DataAnnotations.Schema;
using Relaks.Interfaces;

namespace Relaks.Models;

[Table("Files")]
public abstract class BaseFile : IBaseFile
{
    public Guid Id { get; set; }
    /// <summary>
    /// Системное имя файла вместе с расширением
    /// </summary>
    public string Filename { get; set; } = null!;
    public string MimeType { get; set; } = null!;
    
    /// <summary>
    /// Отображаемое имя файла вместе с расширением
    /// </summary>
    public string DisplayName { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    public string? DeletedReason { get; set; }
    public string Discriminator { get; set; } = null!;
    public string? Keyword { get; set; }
    
    public Guid? CategoryId { get; set; }
    public BaseFileCategory? Category { get; set; }

    public List<BaseFileTag> Tags { get; set; } = new();
    
    public List<BaseEntry> BaseEntryRelations { get; set; } = new();

    public string GetExtensionWithoutDot() => Path.GetExtension(Filename).Replace(".", "");

    protected BaseFile()
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.Now;
    }

    public string DirPath()
    {
        return Path.Combine(
            // "relaks_store",
            // "files",
            CreatedAt.Year.ToString(),
            CreatedAt.Month.ToString(),
            CreatedAt.Day.ToString()
        );
    }

    public string FilePath()
    {
        return Path.Combine(DirPath(), Filename);
    }

    public string DisplayNameWithExtension()
    {
        return DisplayName + Path.GetExtension(Filename);
    }
}

public class EntryFile : BaseFile
{
    public Guid EntryId { get; set; }
    public BaseEntry Entry { get; set; } = null!;
}

// public class EntryInfoFile : BaseFile
// {
//     public Guid EntryInfoId { get; set; }
//     public BaseEntryInfo EntryInfo { get; set; } = null!;
// }

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

public class BaseFileRequest : ISoftDeletedReason
{
    public string DisplayName { get; set; } = null!;
    public DateTime? DeletedAt { get; set; }
    public string? DeletedReason { get; set; }
    public Guid? CategoryId { get; set; }
    public List<Guid> Tags { get; set; } = new();
    public List<Guid> BaseEntryRelationIds { get; set; } = new();
}
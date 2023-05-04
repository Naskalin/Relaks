using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Relaks.Interfaces;

namespace Relaks.Models;

[Table("Entries")]
public abstract class BaseEntry : ITimestamped, ISoftDeletedReason
{
    public Guid Id { get; set; }
    [StringLength(50)]
    public string Discriminator { get; set; } = null!;
    public string Name { get; set; } = null!;
    // public TypeEnum Type { get; set; }
    public string? Description { get; set; }
    public int Reputation { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public DateTime? StartAt { get; set; }
    public DateTime? EndAt { get; set; }

    public DateTime? DeletedAt { get; set; }
    public string? DeletedReason { get; set; }

    // public Guid? Avatar { get; set; }
    //
    public List<BaseEntryInfo> EntryInfos { get; set; } = new();
    // [JsonIgnore]
    // public List<EntryFile> Files { get; set; } = new();
    // [JsonIgnore]
    // public List<Structure> Structures { get; set; } = new();

    public BaseEntry()
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }
}

public class EPerson : BaseEntry
{
    
}

public class ECompany : BaseEntry
{
    
}

public class EMeet : BaseEntry
{
    
}

public class FtsEntry : IFtsEntity
{
    public int RowId { get; set; }
    public string Match { get; set; } = null!;
    public string Snippet { get; set; } = null!;
    public double? Rank { get; set; }
    
    public Guid Id { get; set; }
    public string Body { get; set; } = null!;
    
    
    [NotMapped]
    public BaseEntry? BaseEntry { get; set; }

    public string DeletedAt { get; set; } = null!;
    public string Discriminator { get; set; } = null!;
}

public class BaseEntryRequest
{
    public string Discriminator { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public int Reputation { get; set; }
    public DateTime? StartAt { get; set; }
    public DateTime? EndAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    public string? DeletedReason { get; set; }
}
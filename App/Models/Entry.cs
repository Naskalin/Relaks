using System.Text.Json.Serialization;

namespace App.Models;

public class Entry : BaseEntity, ITimestampResource, ISoftDelete
{
    public string Name { get; set; } = null!;
    public EntryTypeEnum EntryType { get; set; }
    public string Description { get; set; } = null!;
    public int Reputation { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public DateTime? StartAt { get; set; }
    public DateTime? EndAt { get; set; }

    public DateTime? DeletedAt { get; set; }
    public string DeletedReason { get; set; } = null!;

    public Guid? Avatar { get; set; }

    public List<EntryDate> Dates { get; set; } = new();
    public List<EntryNote> Notes { get; set; } = new();
    public List<EntryEmail> Emails { get; set; } = new();
    public List<EntryPhone> Phones { get; set; } = new();
    public List<EntryUrl> Urls { get; set; } = new();
    
    public List<EntryFile> Files { get; set; } = new();
}

public class EntryFts : IFtsEntity
{
    public int RowId { get; set; }
    public string Match { get; set; } = null!;
    public double? Rank { get; set; }
    
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string DeletedReason { get; set; } = null!;
}

public enum EntryTypeEnum
{
    Person,
    Meet,
    Company,
}
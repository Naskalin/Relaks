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

    public List<EntryInfoDate> Dates { get; set; } = new();
    public List<EntryInfoNote> Notes { get; set; } = new();
    public List<EntryInfoEmail> Emails { get; set; } = new();
    public List<EntryInfoPhone> Phones { get; set; } = new();
    public List<EntryInfoUrl> Urls { get; set; } = new();
    
    // public List<EntryDate> Dates { get; set; } = new();
    // public List<EntryText> Texts { get; set; } = new();
    
    public List<EntryTag> Tags { get; set; } = new();

    public List<EntryFile> Files { get; set; } = new();
}

public enum EntryTypeEnum
{
    Person,
    Meet,
    Company,
}
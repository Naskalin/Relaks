using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace App.Models;

public class Entry : BaseEntity, ITimestampResource, IActualResource
{
    public string Name { get; set; } = null!;
    public EntryTypeEnum EntryType { get; set; }
    public string Description { get; set; } = null!;
    public int Reputation { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public DateTime? StartAt { get; set; }
    public DateTime? EndAt { get; set; }

    public DateTime ActualStartAt { get; set; }
    public DateTime? ActualEndAt { get; set; }
    public string ActualStartAtReason { get; set; } = null!;
    public string ActualEndAtReason { get; set; } = null!;

    public List<EntryDate> Dates { get; set; } = new();
    public List<EntryText> Texts { get; set; } = new();
    public List<EntryTag> Tags { get; set; } = new();
}

public enum EntryTypeEnum
{
    Person,
    Meet,
    Company,
}
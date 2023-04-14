using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Relaks.Interfaces;

namespace Relaks.Models;

public class Entry : ITimestamped, ISoftDeleted
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public EntryTypeEnum EntryType { get; set; }
    public string Description { get; set; } = null!;
    public int Reputation { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public DateTime? StartAt { get; set; }
    public DateTime? EndAt { get; set; }

    public DateTime? DeletedAt { get; set; }
    public string? DeletedReason { get; set; } = null!;

    // public Guid? Avatar { get; set; }
    //
    public List<EntryInfo> EntryInfos { get; set; } = new();
    // [JsonIgnore]
    // public List<EntryFile> Files { get; set; } = new();
    // [JsonIgnore]
    // public List<Structure> Structures { get; set; } = new();

    public Entry()
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }
}

// public class FtsEntry : IFtsEntity
// {
//     public int RowId { get; set; }
//     public string Match { get; set; } = null!;
//     public double? Rank { get; set; }
//     
//     public Guid Id { get; set; }
//     
//     public string Data { get; set; } = null!;
// }

public enum EntryTypeEnum
{
    Person,
    Meet,
    Company,
}
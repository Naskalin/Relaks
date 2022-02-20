using System.ComponentModel.DataAnnotations;

namespace App.Models.Entry;

public abstract class BaseEntry: IEntry
{
    [Key] 
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public EntryTypeEnum EntryType { get; set; }
    public int Reputation { get; set; } = 5;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}

public interface IEntry
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public EntryTypeEnum EntryType { get; set; }
    public int Reputation { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

public class BaseEntryDto
{
    public string Name { get; set; } = null!;
    public int Reputation { get; set; }
}

public enum EntryTypeEnum
{
    Person,
    Company,
    Meet,
}
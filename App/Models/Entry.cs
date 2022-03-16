using System.ComponentModel.DataAnnotations;

namespace App.Models;

public class Entry : ITimestampResource, IActualResource
{
    [Key]
    public Guid Id { get; set; }

    [MaxLength(150)]
    [MinLength(2)]
    public string Name { get; set; } = null!;

    [MaxLength(300)]
    public string? Description { get; set; }

    public EntryTypeEnum EntryType { get; set; }

    [Range(1, 10)]
    public int Reputation { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    // Дата рождения/начала встречи/создания компании
    public DateTime? StartAt { get; set; }

    // Дата смерти/окончания встречи/закрытия компании
    public DateTime? EndAt { get; set; }

    public List<EntryDate> Dates { get; set; } = new();
    public List<EntryText> Texts { get; set; } = new();
    public List<EntryTag> Tags { get; set; } = new();

    public DateTime ActualStartAt { get; set; }
    public DateTime? ActualEndAt { get; set; }

    [MaxLength(500)]
    public string? ActualStartAtReason { get; set; }

    [MaxLength(500)]
    public string? ActualEndAtReason { get; set; }
}

public enum EntryTypeEnum
{
    Person,
    Company,
    Meet,
}
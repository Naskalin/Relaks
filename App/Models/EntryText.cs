using System.ComponentModel.DataAnnotations;

namespace App.Models;

public class EntryText : ITimestampResource, IActualResource
{
    [Key]
    public Guid Id { get; set; }
    
    public Entry Entry { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime ActualFrom { get; set; }

    public DateTime? ActualTo { get; set; }

    [MaxLength(200)]
    public string? ActualToReason { get; set; }

    [MaxLength(200)]
    public string? ActualFromReason { get; set; }

    [MaxLength(200)]
    public string? About { get; set; } = null!;

    [MaxLength(5000)]
    public string Val { get; set; } = null!;

    [MaxLength(30)]
    public EntryTextTypeEnum TextType { get; set; }
}

public enum EntryTextTypeEnum
{
    Phone,
    Email,
    Url,
    Note,
}
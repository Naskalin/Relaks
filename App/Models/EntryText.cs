using System.ComponentModel.DataAnnotations;

namespace App.Models;

public class EntryText : ITimestampResource, IActualResource
{
    [Key]
    public Guid Id { get; set; }
    
    public Entry Entry { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string About { get; set; } = null!;
    public string Val { get; set; } = null!;
    public EntryTextTypeEnum TextType { get; set; }
    
    public DateTime ActualStartAt { get; set; }
    public DateTime? ActualEndAt { get; set; }
    public string ActualEndAtReason { get; set; } = null!;
    public string ActualStartAtReason { get; set; } = null!;
}

public enum EntryTextTypeEnum
{
    Phone,
    Email,
    Url,
    Note,
}
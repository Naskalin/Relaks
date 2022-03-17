using System.ComponentModel.DataAnnotations;

namespace App.Models;

public class EntryDate : ITimestampResource, IActualResource
{
    [Key]
    public Guid Id { get; set; }
    
    public Entry Entry { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string About { get; set; } = null!;
    public DateTime Val { get; set; }
    
    public DateTime ActualStartAt { get; set; }
    public DateTime? ActualEndAt { get; set; }
    public string ActualEndAtReason { get; set; } = null!;
    public string ActualStartAtReason { get; set; } = null!;
}
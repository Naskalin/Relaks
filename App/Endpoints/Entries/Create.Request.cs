using System.ComponentModel.DataAnnotations;
using App.Models;

namespace App.Endpoints.Entries;

public class CreateRequest
{
    [MaxLength(150)]
    [MinLength(2)]
    public string Name { get; set; } = null!;

    public EntryTypeEnum EntryType { get; set; }
    
    [MaxLength(300)]
    public string? Description { get; set; }

    [Range(1, 10)]
    public int? Reputation { get; set; }

    public DateTime? StartAt { get; set; }

    public DateTime? EndAt { get; set; }
    
    public DateTime? ActualStartAt { get; set; }
    public DateTime? ActualEndAt { get; set; }

    [MaxLength(500)]
    public string? ActualStartAtReason { get; set; }

    [MaxLength(500)]
    public string? ActualEndAtReason { get; set; }
}
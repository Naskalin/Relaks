using System.ComponentModel.DataAnnotations;

namespace App.Models;

public class EntryDate : ITimestampResource, IActualResource
{
    [Key]
    public Guid Id { get; set; }
    
    public Entry Entry { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime ActualStartAt { get; set; }

    public DateTime? ActualEndAt { get; set; }

    [MaxLength(200)]
    public string? ActualEndAtReason { get; set; }

    [MaxLength(200)]
    public string? ActualStartAtReason { get; set; }

    [MaxLength(200)]
    public string? About { get; set; } = null!;

    public DateTime Val { get; set; }

    [MaxLength(30)]
    public EntryDateTypeEnum? DateType { get; set; }
}

public enum EntryDateTypeEnum
{
    // PersonBirthday,
    // PersonDeath,
    FirstMeet,
    // MeetStart,
    // MeetEnd,
    // CompanyCreation,
    // CompanyClosing,
}
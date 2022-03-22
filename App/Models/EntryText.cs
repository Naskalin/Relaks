namespace App.Models;

public class EntryText : BaseEntity, ITimestampResource, IActualResource
{
    public Entry Entry { get; set; } = null!;
    public Guid EntryId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string About { get; set; } = null!;
    public string Val { get; set; } = null!;
    public TextTypeEnum TextType { get; set; }
    
    public DateTime ActualStartAt { get; set; }
    public DateTime? ActualEndAt { get; set; }
    public string ActualEndAtReason { get; set; } = null!;
    public string ActualStartAtReason { get; set; } = null!;
}

public enum TextTypeEnum
{
    Phone,
    Email,
    Url,
    Note,
}
using Relaks.Interfaces;

namespace Relaks.Models.StructureModels;

public class StructureItem : ISoftDeletedReason
{
    public Guid Id { get; set; }
    
    public Guid EntryId { get; set; }
    public BaseEntry Entry { get; set; } = null!;

    public string? Title { get; set; }
    public string? Description { get; set; }
    
    public DateTime StartAt { get; set; }
    public DateTime? EndAt { get; set; }
    
    public DateTime? DeletedAt { get; set; }
    public string? DeletedReason { get; set; }
}
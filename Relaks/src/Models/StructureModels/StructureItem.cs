using Relaks.Interfaces;

namespace Relaks.Models.StructureModels;

public class StructureItem
{
    public Guid Id { get; set; }
    
    public Guid EntryId { get; set; }
    public BaseEntry Entry { get; set; } = null!;

    public string? Title { get; set; }
    public string? Description { get; set; }
    
    public DateTime StartAt { get; set; }
    public DateTime? EndAt { get; set; }
    
    public StructureGroup Group { get; set; } = null!;
    public Guid GroupId { get; set; }

    public StructureItem()
    {
        Id = Guid.NewGuid();
    }
}
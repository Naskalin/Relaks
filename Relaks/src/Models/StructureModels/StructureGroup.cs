using Relaks.Interfaces;

namespace Relaks.Models.StructureModels;

public class StructureGroup
{
    public Guid Id { get; set; }
    public string? Title { get; set; } = null!;
    public string? Description { get; set; }
    
    public Guid EntryId { get; set; }
    public BaseEntry Entry { get; set; } = null!;
 
    public List<StructureItem> Items { get; set; } = new();

    public Guid? ParentId { get; set; }
    public StructureGroup? Parent { get; set; }
    
    public List<StructureGroup> Children { get; set; } = new();
    
    public DateTime StartAt { get; set; }
    public DateTime? EndAt { get; set; }
}
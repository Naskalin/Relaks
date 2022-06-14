using System.Text.Json.Serialization;

namespace App.Models;

public class Structure : BaseEntity
{
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTime StartAt { get; set; }
    public DateTime? EndAt { get; set; }
    
    public List<StructureItem> Items { get; set; } = new();
    
    [JsonIgnore]
    public Structure? Parent { get; set; }
    public Guid? ParentId { get; set; }
    
    [JsonIgnore]
    public Entry Entry { get; set; } = null!;
    public Guid EntryId { get; set; }
}

public class StructureItem : BaseEntity
{
    public string Comment { get; set; } = null!;
    public DateTime StartAt { get; set; }
    public DateTime? EndAt { get; set; }
    
    public Entry Entry { get; set; } = null!;
    public Guid EntryId { get; set; }

    [JsonIgnore]
    public Structure Structure { get; set; } = null!;
    public Guid StructureId { get; set; }
}
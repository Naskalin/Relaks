namespace App.Models;

public class Structure : BaseEntity
{
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public List<StructureGroup> Groups = new();
}

public class StructureGroup
{
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public List<StructureItem> Items { get; set; } = new();
}

public class StructureItem
{
    public string Comment { get; set; } = null!;
    public Guid EntryId { get; set; }
    public DateTime StartAt { get; set; }
    public DateTime EndAt { get; set; }
}
using Relaks.Database;
using Relaks.Database.Repositories;
using Relaks.Models.StructureModels;

namespace Relaks.Models.Store;

public class StructureDiagramOptions
{
    public int DiagramScale { get; set; } = 100;
    public int LimitStructureItems { get; set; } = 3;
    public bool IsShowStructureItems { get; set; } = true;
    public bool IsShowDates { get; set; }
}

public class EntryStructureStore
{
    public StructureDiagramOptions DiagramOptions { get; set; } = new();
    
    public StructureGroup? SelectedGroup { get; set; }
    public StructureItem? SelectedItem { get; set; }
    private readonly AppDbContext _db;
    public string Discriminator { get; set; } = null!;
    public List<StructureGroup> StructureGroups { get; set; } = new();
    public StructureGroupListRequest Req { get; set; } = new();

    public EntryStructureStore(AppDbContext db)
    {
        _db = db;
    }

    public void FindStructure()
    {
        StructureGroups = _db.StructureGroups.ToTree(Req);
    }
}
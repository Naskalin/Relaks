using Relaks.Database;
using Relaks.Database.Repositories;
using Relaks.Models.StructureModels;

namespace Relaks.Models.Store;

public class EntryStructureStore
{
    public int DiagramScale { get; set; } = 100;
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
using LinqKit;
using Microsoft.EntityFrameworkCore;
using Relaks.Database;
using Relaks.Models.StructureModels;

namespace Relaks.Models.Store;

public class EntryRelationRequest
{
    public Guid? FirstId { get; set; }
    public Guid? SecondId { get; set; }
    public int FirstRating { get; set; }
    public int SecondRating { get; set; }
    public string? Description { get; set; }
    // public string? FirstDescription { get; set; }
    // public string? SecondDescription { get; set; }
}

public class EntryConnectionStore
{
    public enum StateEnum
    {
        List,
        NewEntryConnection,
        EditEntryConnection,
    }
    public EntryRelation? EntryRelationEdit { get; set; }
    public Guid EntryId { get; set; }
    private readonly AppDbContext _db;
    public StateEnum State { get; set; }

    public List<StructureItem> StructureItems { get; set; } = new();
    public Dictionary<Guid, string> StructureGroupTitles { get; set; } = new();
    public List<EntryRelation> EntryRelations { get; set; } = new();

    public EntryConnectionStore(AppDbContext db)
    {
        _db = db;
        State = StateEnum.List;
    }

    public void FindEntryRelations()
    {
        EntryRelations = _db.EntryRelations
            .Where(x => x.FirstId.Equals(EntryId) || x.SecondId.Equals(EntryId))
            .ToList();
    }

    public void FindStructureGroupTitles()
    {
        var groupIds = new List<Guid>();
        
        StructureItems
            .Select(x => x.Group)
            .Select(g => g.TreePath)
            .ForEach(treePath =>
            {
                foreach (var strGuid in treePath.Split("/"))
                {
                    if (Guid.TryParse(strGuid, out var groupId) && groupId != default && !groupIds.Contains(groupId))
                    {
                        groupIds.Add(groupId);
                    }
                }
            });
        
        _db.StructureGroups.Where(x => groupIds.Contains(x.Id)).ToList().ForEach(x =>
        {
            StructureGroupTitles[x.Id] = x.Title;
        });
    }

    public void FindStructureItems()
    {
        StructureItems = _db.StructureItems
            .Include(x => x.Group)
            .Where(x => x.EntryId.Equals(EntryId))
            .ToList();
    }
}
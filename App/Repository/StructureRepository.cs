using App.DbConfigurations;
using App.Endpoints.Structures;
using App.Models;
using App.Utils;
using Microsoft.EntityFrameworkCore;

namespace App.Repository;

public class StructureRepository : BaseRepository<Structure>
{
    public StructureRepository(AppDbContext db) : base(db)
    {
    }

    public async Task<TreeExtensions.ITree<Structure>> GetTreeForEntry(
        ListRequest req,
        CancellationToken cancellationToken
    )
    {
        var query = Entities
            .Where(x => x.EntryId.Equals(req.EntryId));

        if (req.IsDeleted != null)
        {
            if (req.IsDeleted == true)
            {
                query = query.Where(x => x.DeletedAt != null && x.DeletedAt > DateTime.UtcNow);
            }
            else
            {
                // false
                query = query.Where(x => x.DeletedAt == null || x.DeletedAt < DateTime.UtcNow);
            }
        }

        if (req.Date != null)
        {
            query = query.Where(x => x.StartAt <= req.Date);
        }

        var structures = await query
            .Include(x => x.Items)
            .ThenInclude(x => x.Entry)
            .ToListAsync(cancellationToken);

        TreeExtensions.ITree<Structure>
            tree = structures.ToTree((parent, child) => child.ParentId == parent.Id);

        return tree;
    }
}
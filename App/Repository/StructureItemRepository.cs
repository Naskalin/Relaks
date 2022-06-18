using App.DbConfigurations;
using App.Endpoints.StructureItems;
using App.Models;
using Microsoft.EntityFrameworkCore;

namespace App.Repository;

public class StructureItemRepository : BaseRepository<StructureItem>
{
    public StructureItemRepository(AppDbContext db) : base(db)
    {
    }
    
    public async Task<StructureItem?> FindByIdWithRelationsAsync(Guid id, CancellationToken cancellationToken)
    {
        return await Entities
            .Include(x => x.Entry)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<List<StructureItem>> FindByListRequestAsync(ListRequest req, CancellationToken cancellationToken)
    {
        var query = Entities.AsQueryable();

        if (req.EntryId != null)
            query = query.Where(x => x.EntryId.Equals(req.EntryId));

        if (req.StructureId != null)
            query = query.Where(x => x.StructureId.Equals(req.StructureId));

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

        query = query.Include(x => x.Entry);
        if (req.EntryType != null)
            query = query.Where(x => x.Entry.EntryType.Equals(req.EntryType));
        
        if (req.Page != null && req.PerPage != null)
        {
            query = PaginateQuery(query, req);     
        }

        return await query.ToListAsync(cancellationToken);
    }
}
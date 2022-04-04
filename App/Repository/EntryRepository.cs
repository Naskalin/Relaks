using App.DbConfigurations;
using App.Endpoints.Entries;
using App.Models;
using App.Utils.Extensions.Database;
using Microsoft.EntityFrameworkCore;

namespace App.Repository;

public class EntryRepository : BaseRepository<Entry>
{
    public EntryRepository(AppDbContext db) : base(db)
    {
    }
    
    public async Task<IEnumerable<Entry>> PaginateListAsync(ListRequest req, CancellationToken cancellationToken)
    {
        var query = Entities.Where(x => req.Deleted == true ? x.DeletedAt != null : x.DeletedAt == null);

        if (req.EntryType != null)
        {
            query = query.Where(x => x.EntryType == req.EntryType);
        }
        
        if (req.Search != null)
        {
            query = query.Where(x =>
                EF.Functions.Like(x.Name, "%" + req.Search + "%")
                || EF.Functions.Like(x.Description, "%" + req.Search + "%")
            );
        }
        
        if (req.OrderBy != null)
        {
            query = query.OrderBy(req.OrderBy, req.OrderByDesc ?? false);
        }

        query = query.OrderByDescending(x => x.UpdatedAt);

        query = PaginateQuery(query, req);
        return await query.ToListAsync(cancellationToken);
    }
}
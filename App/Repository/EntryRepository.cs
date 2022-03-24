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
    
    public async Task<IEnumerable<Entry>> PaginateListAsync(ListRequest listRequest, CancellationToken cancellationToken)
    {
        var query = Entities.Where(x => true);

        if (listRequest.EntryType != null)
        {
            query = query.Where(x => x.EntryType == listRequest.EntryType);
        }
        
        if (listRequest.Search != null)
        {
            query = query.Where(x =>
                EF.Functions.Like(x.Name, "%" + listRequest.Search + "%")
                || (EF.Functions.Like(x.Description, "%" + listRequest.Search + "%"))
            );
        }
        
        if (listRequest.OrderBy != null)
        {
            query = query.OrderBy(listRequest.OrderBy, listRequest.OrderByDesc ?? false);
        }
        
        query = query.OrderByDescending(x => x.UpdatedAt);
        
        
        query = PaginateQuery(query, listRequest);
        return await query.ToListAsync(cancellationToken);
    }
}
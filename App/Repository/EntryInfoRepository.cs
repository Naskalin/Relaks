using App.DbConfigurations;
using App.Endpoints.Entries.EntryInfos;
using App.Models;
using App.Utils.Extensions.Database;
using Microsoft.EntityFrameworkCore;

namespace App.Repository;

public class EntryInfoRepository : BaseRepository<EntryInfo>
{
    public EntryInfoRepository(AppDbContext db) : base(db)
    {
    }
    
    public async Task<List<EntryInfo>> PaginateAsync(
        EntryInfoListDeletableRequest deletableRequest,
        CancellationToken cancellationToken
    )
    {
        var query = Entities.Where(x => x.EntryId == deletableRequest.EntryId);

        if (deletableRequest.Type != null && deletableRequest.Type.Any())
        {
            var types = deletableRequest.Type.Select(x => x.ToUpper()).ToList();
            query = query.Where(x => types.Contains(x.Type));
        }

        if (deletableRequest.IsDeleted != null)
            query = query.Where(x => deletableRequest.IsDeleted == true ? x.DeletedAt != null : x.DeletedAt == null);
        
        //TODO: Search
        // if (!string.IsNullOrEmpty(request.Search))
        //     query = query.Where(x => EF.Functions.Like(x.Title, "%" + request.Search + "%")
        //                              || EF.Functions.Like(x.Email, "%" + request.Search + "%")
        //                              || EF.Functions.Like(x.DeletedReason, "%" + request.Search + "%")
        //     );

        if (!string.IsNullOrEmpty(deletableRequest.OrderBy))
        {
            query = query.OrderBy(deletableRequest.OrderBy, deletableRequest.OrderByDesc ?? false);
        }
        else
        {
            query = query.OrderByDescending(x => x.UpdatedAt);
        }
        
        if (deletableRequest.Page != null && deletableRequest.PerPage != null)
        {
            query = PaginateQuery(query, deletableRequest);     
        }
        
        return await query.ToListAsync(cancellationToken);
    }
}
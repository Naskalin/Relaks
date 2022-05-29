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
        EntryInfoListRequest request,
        CancellationToken cancellationToken
    )
    {
        var query = Entities.Where(x => x.EntryId == request.EntryId);

        if (request.Type != null)
            query = query.Where(x => x.Type == request.Type);

        if (request.IsDeleted != null)
            query = query.Where(x => request.IsDeleted == true ? x.DeletedAt != null : x.DeletedAt == null);
        
        //TODO: Search
        // if (!string.IsNullOrEmpty(request.Search))
        //     query = query.Where(x => EF.Functions.Like(x.Title, "%" + request.Search + "%")
        //                              || EF.Functions.Like(x.Email, "%" + request.Search + "%")
        //                              || EF.Functions.Like(x.DeletedReason, "%" + request.Search + "%")
        //     );

        if (!string.IsNullOrEmpty(request.OrderBy))
        {
            query = query.OrderBy(request.OrderBy, request.OrderByDesc ?? false);
        }
        else
        {
            query = query.OrderByDescending(x => x.UpdatedAt);
        }
        
        if (request.Page != null && request.PerPage != null)
        {
            query = PaginateQuery(query, request);     
        }
        
        return await query.ToListAsync(cancellationToken);
    }
}
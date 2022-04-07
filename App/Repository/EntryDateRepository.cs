using App.DbConfigurations;
using App.Endpoints.Entries.EntryInfos;
using App.Models;
using Microsoft.EntityFrameworkCore;
using App.Utils.Extensions.Database;

namespace App.Repository;

public class EntryDateRepository : BaseRepository<EntryDate>
{
    public EntryDateRepository(AppDbContext db) : base(db)
    {
    }

    public async Task<List<EntryDate>> PaginateListAsync(
        EntryInfoListRequest request,
        CancellationToken cancellationToken)
    {
        var query = Entities.Where(x => x.EntryId == request.EntryId);
        
        if (request.isDeleted != null)
            query = query.Where(x => request.isDeleted == true ? x.DeletedAt != null : x.DeletedAt == null);
        
        if (!string.IsNullOrEmpty(request.Search))
        {
            query = query.Where(x => 
                EF.Functions.Like(x.Title, "%" + request.Search + "%")
                || EF.Functions.Like(x.DeletedReason, "%" + request.Search + "%")
                );
        }

        if (!string.IsNullOrEmpty(request.OrderBy))
        {
            query = query.OrderBy(request.OrderBy, request.OrderByDesc ?? false);
        }
        else
        {
            query = query.OrderByDescending(x => x.Date);
        }

        if (request.Page != null && request.PerPage != null)
        {
            query = PaginateQuery(query, request);     
        }

        return await query.ToListAsync(cancellationToken);
    }
}
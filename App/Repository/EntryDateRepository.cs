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
        EInfoListRequest request,
        CancellationToken cancellationToken)
    {
        var query = Entities.Where(x => x.EntryId == request.EntryId)
            .Where(x => request.Deleted == true ? x.DeletedAt != null : x.DeletedAt == null);
        if (request.Search != null) query = query.Where(x => EF.Functions.Like(x.Title, "%" + request.Search + "%"));

        if (request.OrderBy != null)
        {
            query = query.OrderBy(request.OrderBy, request.OrderByDesc ?? false);
        }
        else
        {
            query = query.OrderByDescending(x => x.Date);
        }

        query = PaginateQuery(query, request);

        return await query.ToListAsync(cancellationToken);
    }
}
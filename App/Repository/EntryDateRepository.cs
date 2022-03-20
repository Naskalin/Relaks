using App.DbConfigurations;
using App.Endpoints.Entries.Dates;
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
        ListRequest listRequest,
        CancellationToken cancellationToken)
    {
        var query = PaginateQuery(listRequest)
            .Where(x => x.Entry.Id == listRequest.EntryId);

        if (listRequest.Search != null)
        {
            query = query.Where(x => EF.Functions.Like(x.About, "%" + listRequest.Search + "%"));
        }

        if (listRequest.OrderBy != null)
        {
            query = query.OrderBy(listRequest.OrderBy, listRequest.OrderByDesc ?? false);
        }

        query = query.OrderByDescending(x => x.UpdatedAt);

        return await query.ToListAsync(cancellationToken);
    }
    // public override List<EntryDate> PaginateListAsync()
    // {
    //     
    // }
}
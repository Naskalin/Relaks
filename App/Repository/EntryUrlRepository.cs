﻿using App.DbConfigurations;
using App.Endpoints.Entries.EntryInfos;
using App.Models;
using App.Utils.Extensions.Database;
using Microsoft.EntityFrameworkCore;

namespace App.Repository;

public class EntryUrlRepository : BaseRepository<EntryUrl>
{
    public EntryUrlRepository(AppDbContext db) : base(db)
    {
    }

    public async Task<List<EntryUrl>> PaginateListAsync(
        EntryInfoListRequest request,
        CancellationToken cancellationToken
    )
    {
        var query = Entities.Where(x => x.EntryId == request.EntryId)
            .Where(x => request.isDeleted == true ? x.DeletedAt != null : x.DeletedAt == null);
        
        if (request.Search != null)
            query = query.Where(x => EF.Functions.Like(x.Title, "%" + request.Search + "%")
                                     || EF.Functions.Like(x.Url, "%" + request.Search + "%")
                                     || EF.Functions.Like(x.DeletedReason, "%" + request.Search + "%")
            );

        if (request.OrderBy != null)
        {
            query = query.OrderBy(request.OrderBy, request.OrderByDesc ?? false);
        }
        else
        {
            query = query.OrderByDescending(x => x.UpdatedAt);
        }

        query = PaginateQuery(query, request);

        return await query.ToListAsync(cancellationToken);
    }
}
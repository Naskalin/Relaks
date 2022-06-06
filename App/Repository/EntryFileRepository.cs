using App.DbConfigurations;
using App.Endpoints.Entries.EntryFiles;
using App.Endpoints.Entries.EntryFiles.Meta;
using App.Models;
using App.Utils.Extensions.Database;
using Microsoft.EntityFrameworkCore;

namespace App.Repository;

public class EntryFileRepository : BaseRepository<EntryFile>
{
    public EntryFileRepository(AppDbContext db) : base(db)
    {
    }

    public async Task<List<EntryFile>> PaginateListAsync(
        ListRequest request,
        CancellationToken cancellationToken)
    {
        var query = Entities.Where(x => x.EntryId == request.EntryId);

        if (request.IsDeleted != null)
            query = query.Where(x => request.IsDeleted == true ? x.DeletedAt != null : x.DeletedAt == null);

        if (request.Category != null)
        {
            if (request.Category == "")
                query = query.Where(x => x.Category == "");
            else
                query = query.Where(x => x.Category == request.Category);   
        }

        if (request.Tags.Any())
        {
            var fileIds = query
                    .AsEnumerable()
                    .Where(x => x.Tags.Any(tag => request.Tags.Contains(tag)))
                    .Select(x => x.Id)
                    .ToList()
                ;
            query = query.Where(x => fileIds.Contains(x.Id));
        }

        if (!string.IsNullOrEmpty(request.Search))
            query = query.Where(x => EF.Functions.Like(x.Name, "%" + request.Search + "%")
                                     || EF.Functions.Like(x.DeletedReason, "%" + request.Search + "%")
            );

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

    public async Task<GetMetaResult> GetEntryMetaAsync(Guid entryId, CancellationToken cancellationToken)
    {
        var allMeta = await Entities
            .Where(x => x.EntryId == entryId)
            .Select(x => new {x.Tags, x.Category})
            .ToListAsync(cancellationToken);

        var categories = allMeta
                .Where(x => !String.IsNullOrEmpty(x.Category))
                .Select(x => x.Category)
                .ToHashSet()
                .OrderBy(x => x)
                .ToList()
            ;

        var tags = new HashSet<string>();
        var tagLists = allMeta.Select(x => x.Tags).ToList();
        foreach (var tagList in tagLists)
        {
            foreach (var tag in tagList)
            {
                tags.Add(tag);
            }
        }
        
        var result = new GetMetaResult()
        {
            Categories = categories,
            Tags = tags.OrderBy(x => x).ToList()
        };

        return result;
    }
}
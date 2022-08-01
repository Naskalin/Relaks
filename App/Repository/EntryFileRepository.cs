using App.DbConfigurations;
using App.Endpoints.EntryFiles;
using App.Endpoints.EntryFiles.Meta;
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
        EntryFileListRequest req,
        CancellationToken cancellationToken)
    {
        var query = Entities
            .Include(x => x.Category)
            .Where(x => x.EntryId == req.EntryId);

        if (req.IsDeleted != null)
            query = query.Where(x => req.IsDeleted == true ? x.DeletedAt != null : x.DeletedAt == null);

        if (req.CategoryId != null)
        {
            if (req.CategoryId == default)
                query = query.Where(x => x.CategoryId == null);
            else
                query = query.Where(x => x.CategoryId == req.CategoryId);   
        }

        if (req.Tags.Any())
        {
            var fileIds = query
                    .AsEnumerable()
                    .Where(x => x.Tags.Any(tag => req.Tags.Contains(tag)))
                    .Select(x => x.Id)
                    .ToList()
                ;
            query = query.Where(x => fileIds.Contains(x.Id));
        }

        if (!string.IsNullOrEmpty(req.Search))
            query = query.Where(x => EF.Functions.Like(x.Name, "%" + req.Search + "%")
                                     || EF.Functions.Like(x.DeletedReason, "%" + req.Search + "%")
            );

        if (!string.IsNullOrEmpty(req.OrderBy))
        {
            query = query.OrderBy(req.OrderBy, req.OrderByDesc ?? false);
        }
        else
        {
            query = query.OrderByDescending(x => x.UpdatedAt);
        }

        if (req.Page != null && req.PerPage != null)
        {
            query = PaginateQuery(query, req);
        }

        return await query.ToListAsync(cancellationToken);
    }

    public async Task<GetMetaResult> GetEntryMetaAsync(Guid entryId, CancellationToken cancellationToken)
    {
        var allMeta = await Entities
            .Where(x => x.EntryId == entryId)
            .Select(x => new {x.Tags, x.Category})
            .ToListAsync(cancellationToken);

        // var categories = allMeta
        //         .Where(x => !String.IsNullOrEmpty(x.Category))
        //         .Select(x => x.Category)
        //         .ToHashSet()
        //         .OrderBy(x => x)
        //         .ToList()
        //     ;

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
            // Categories = categories,
            Tags = tags.OrderBy(x => x).ToList()
        };

        return result;
    }
}
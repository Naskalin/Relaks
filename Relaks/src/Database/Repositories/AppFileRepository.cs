using Relaks.Interfaces;
using Relaks.Models;
using Relaks.Models.Misc;
using Relaks.Utils.Extensions;

namespace Relaks.Database.Repositories;

public class AppFileFindRequest : IOrderable
{
    public string? Search { get; set; }
    public Guid? CategoryId { get; set; }
    public Guid? EntryId { get; set; }
    public string? Discriminator { get; set; }
    public List<Guid> TagIds { get; set; } = new();
    public string? Keyword { get; set; }
    public string? OrderBy { get; set; }
    public bool? IsOrderByDesc { get; set; }
    public bool IsDeleted { get; set; }
}

public class AppFileFindResult
{
    public BaseFile BaseFile { get; set; } = null!;
    public FtsFile? FtsFile { get; set; }
}

public static class EntryFileRepository
{
    public static TotalResult<AppFileFindResult> FindFiles(this AppDbContext db, AppFileFindRequest req)
    {
        if (!string.IsNullOrEmpty(req.Search)) return FindFtsFiles(db, req);

        var q = db.BaseFiles.AsQueryable();
        
        if (req.EntryId.HasValue)
        {
            var entryFileIdQuery = db.EntryFiles.Where(x => x.EntryId.Equals(req.EntryId.Value)).Select(x => x.Id);
            q = q.Where(x => entryFileIdQuery.Contains(x.Id));
        }
        
        q = req.IsDeleted ? q.Where(x => x.DeletedAt != null) : q.Where(x => x.DeletedAt == null);
        

        if (req.Discriminator != null)
        {
            q = q.Where(x => x.Discriminator.Equals(req.Discriminator));
        }

        if (req.CategoryId.HasValue)
        {
            q = q.Where(x => x.CategoryId.Equals(req.CategoryId.Value));
        }
        
        if (req.TagIds.Any())
        {
            q = q.Where(x => x.Tags.Any(t => req.TagIds.Equals(t.Id)));
        }
        
        if (!string.IsNullOrEmpty(req.Keyword))
        {
            q = q.Where(x => x.Keyword != null && x.Keyword.Equals(req.Keyword));
        }

        q = string.IsNullOrEmpty(req.OrderBy) ? q.OrderByDescending(x => x.CreatedAt) : q.OrderBy(req);

        var total = q.ToTotalResult();

        return new TotalResult<AppFileFindResult>()
        {
            Total = total.Total,
            Items = total.Items.Select(x => new AppFileFindResult {BaseFile = x}).ToList()
        };
    }
    
    private static TotalResult<AppFileFindResult> FindFtsFiles(this AppDbContext db, AppFileFindRequest req)
    {
        if (string.IsNullOrEmpty(req.Search)) return new TotalResult<AppFileFindResult>();

        var s = $"\"{req.Search}\"*";
        
        var q = db.Set<FtsFile>().Where(x => x.Match == s);
        
        q = req.IsDeleted
            ? q.Where(x => !string.IsNullOrEmpty(x.DeletedAt))
            : q.Where(x => string.IsNullOrEmpty(x.DeletedAt));
        
        if (!string.IsNullOrEmpty(req.Discriminator))
        {
            q = q.Where(x => x.Discriminator.Equals(req.Discriminator));
        }
        
        if (req.EntryId.HasValue)
        {
            var entryFileIdsQuery = db.EntryFiles.Where(x => x.EntryId.Equals(req.EntryId.Value)).Select(x => x.Id.ToString()).ToList();
            q = q.Where(x => entryFileIdsQuery.Contains(x.Id.ToString()));
        }

        q = q.Select(x => new FtsFile()
                {
                    Id = x.Id,
                    Rank = x.Rank,
                    Snippet = db.Snippet(x.Match, "-1", "<b>", "</b>", "...", 5),
                    Discriminator = x.Discriminator,
                    DeletedAt = x.DeletedAt,
                })
            ;

        q = q.OrderByDescending(x => x.Rank);
        
        var total = q.ToTotalResult();
        
        var fileIds = total.Items.Select(x => x.Id).ToList();
        var appFiles = db.BaseFiles.Where(x => fileIds.Contains(x.Id)).ToDictionary(x => x.Id, x => x);
        return new TotalResult<AppFileFindResult>()
        {
            Total = total.Total,
            Items = total.Items.Select(x => new AppFileFindResult {BaseFile = appFiles[x.Id], FtsFile = x,}).ToList()
        };
    }
}
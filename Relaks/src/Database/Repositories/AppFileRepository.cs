using Microsoft.EntityFrameworkCore;
using Relaks.Interfaces;
using Relaks.Models;
using Relaks.Models.Misc;
using Relaks.Utils.Extensions;

namespace Relaks.Database.Repositories;

public class AppFileFindRequest : IOrderable
{
    public string? Search { get; set; }
    public Guid? CategoryId { get; set; }
    public bool IsWithoutCategory { get; set; }
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
    private static IQueryable<BaseFile> FindBaseFiles(this AppDbContext db, AppFileFindRequest req)
    {
        var q = db.BaseFiles
            .Include(x => x.Tags)
            .Include(x => x.Category)
            .AsQueryable();
        
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
            // если выбрана категория, то выводим все файлы вложенные в дочерние категории при клике на родительскую
            var touchedCategoryIds = db.BaseFileCategories.FindTouchedCategories(req.CategoryId.Value).Select(x => x.Id);
            q = q.Where(x => x.CategoryId.HasValue && touchedCategoryIds.Contains(x.CategoryId.Value));
        }
        else if (req.IsWithoutCategory)
        {
            q = q.Where(x => x.CategoryId.Equals(null));
        }

        if (req.TagIds.Any())
        {
            q = q.Where(x => x.Tags.Any(t => req.TagIds.Contains(t.Id)));
        }

        if (!string.IsNullOrEmpty(req.Keyword))
        {
            q = q.Where(x => x.Keyword != null && x.Keyword.Equals(req.Keyword));
        }

        q = string.IsNullOrEmpty(req.OrderBy) ? q.OrderByDescending(x => x.CreatedAt) : q.OrderBy(req);

        return q;
    }
    
    public static TotalResult<AppFileFindResult> FindFiles(this AppDbContext db, AppFileFindRequest req)
    {
        if (!string.IsNullOrEmpty(req.Search)) return FindFtsFiles(db, req);
        
        var total = FindBaseFiles(db, req).ToTotalResult();
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

        var ftsQuery = db.Set<FtsFile>().Where(x => x.Match == s);

        ftsQuery = ftsQuery.Select(x => new FtsFile()
                {
                    Id = x.Id,
                    Rank = x.Rank,
                    Snippet = db.Snippet(x.Match, "-1", "<b>", "</b>", "...", 5),
                    Discriminator = x.Discriminator,
                    DeletedAt = x.DeletedAt,
                })
            ;

        var ftsResult = ftsQuery.ToList();
        ftsResult = ftsResult
            .Where(x => FindBaseFiles(db, req).Select(x => x.Id).Contains(x.Id))
            .OrderByDescending(x => x.Rank)
            .ToList();

        var baseFileIds = ftsResult.Select(x => x.Id);
        var appFiles = db.BaseFiles.Where(x => baseFileIds.Contains(x.Id)).ToDictionary(x => x.Id, x => x);
        return new TotalResult<AppFileFindResult>()
        {
            Total = ftsResult.Count,
            Items = ftsResult.Select(x => new AppFileFindResult {BaseFile = appFiles[x.Id], FtsFile = x,}).ToList()
        };
    }
}
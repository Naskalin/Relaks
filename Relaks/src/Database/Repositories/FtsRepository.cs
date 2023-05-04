using Relaks.Models;

namespace Relaks.Database.Repositories;

public class FtsSearchAllRequest
{
    public string? Search { get; set; }
    public bool? IsDeleted { get; set; }
}

public class FtsSearchAllResult
{
    public double Rank { get; set; }
    public string Snippet { get; set; } = null!;
    public Guid? EntryId { get; set; }
    public Guid? EntryInfoId { get; set; }
    public string EntityName { get; set; } = null!;
    public string FtsEntityName { get; set; } = null!;
    public string DeletedAt { get; set; } = null!;
}

public static class FtsRepository
{
    public static List<FtsSearchAllResult> FtsSearchAll(this AppDbContext db, FtsSearchAllRequest req)
    {
        var search = req.Search != null ? req.Search.Trim() : "";
        if (string.IsNullOrEmpty(search)) return new List<FtsSearchAllResult>();
        
        search = $"\"{req.Search}\"*";
        var queryEntries = db.Set<FtsEntry>().Where(x => x.Match == search);
        queryEntries = req.IsDeleted == true
            ? queryEntries.Where(x => !string.IsNullOrEmpty(x.DeletedAt))
            : queryEntries.Where(x => string.IsNullOrEmpty(x.DeletedAt));
        
        var ftsEntries = queryEntries
            .Select(x => new FtsSearchAllResult()
            {
                EntryId = x.Id,
                Rank = x.Rank ?? 0,
                Snippet = db.Snippet(x.Match, "-1", "<b>", "</b>", "...", 5),
                FtsEntityName = nameof(FtsEntry),
                EntityName = x.Discriminator,
                DeletedAt = x.DeletedAt
            })
            ;

        var queryEntryInfos = db.Set<FtsEntryInfo>().Where(x => x.Match == search);
    
        queryEntryInfos = req.IsDeleted == true
            ? queryEntryInfos.Where(x => !string.IsNullOrEmpty(x.DeletedAt))
            : queryEntryInfos.Where(x => string.IsNullOrEmpty(x.DeletedAt));
        
        var ftsEntryInfos = queryEntryInfos
                .Select(x => new FtsSearchAllResult()
                {
                    EntryInfoId = x.Id,
                    EntryId = x.EntryId,
                    Rank = x.Rank ?? 0,
                    Snippet = db.Snippet(x.Match, "-1", "<b>", "</b>", "...", 5),
                    FtsEntityName = nameof(FtsEntryInfo),
                    EntityName = x.Discriminator,
                    DeletedAt = x.DeletedAt,
                })
            ;

        var ftsUnion = ftsEntries
            .AsEnumerable()
            .Union(ftsEntryInfos)
            .OrderByDescending(x => x.Rank)
            .Take(12)
            .ToList();

        return ftsUnion;
    }
}
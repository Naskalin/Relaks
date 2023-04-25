using Relaks.Models;

namespace Relaks.Database.Repostitories;

public class FtsSearchResult
{
    public double Rank { get; set; }
    public string Snippet { get; set; } = null!;
    public Guid? EntryId { get; set; }
    public Guid? EntryInfoId { get; set; }
    public string EntityName { get; set; } = null!;
    public string FtsEntityName { get; set; } = null!;
}

public static class FtsRepository
{
    public static List<FtsSearchResult> FtsSearch(this AppDbContext db, string search)
    {
        var s = $"\"{search}\"*";
        var ftsEntries = db.Set<FtsEntry>()
            .Where(x => x.Match == s)
            .Select(x => new FtsSearchResult()
            {
                EntryId = x.Id,
                Rank = x.Rank ?? 0,
                Snippet = db.Snippet(x.Match, "-1", "<b>", "</b>", "...", 5),
                FtsEntityName = nameof(FtsEntry)
            })
            ;

        var ftsEntryInfos = db.Set<FtsEntryInfo>()
                .Where(x => x.Match == s)
                .Select(x => new FtsSearchResult()
                {
                    EntryInfoId = x.Id,
                    EntryId = x.EntryId,
                    Rank = x.Rank ?? 0,
                    Snippet = db.Snippet(x.Match, "-1", "<b>", "</b>", "...", 5),
                    FtsEntityName = nameof(FtsEntryInfo)
                })
            ;

        var ftsUnion = ftsEntries
            .AsEnumerable()
            .Union(ftsEntryInfos)
            .OrderBy(x => x.Rank)
            .Take(15)
            .ToList();

        List<Guid> entryIds = ftsUnion
            .Where(x => x.EntryId.HasValue)
            .Select(x => x.EntryId!.Value)
            .ToList()
            ;
        var entries = db.Entries.Where(x => entryIds.Contains(x.Id)).ToDictionary(x => x.Id, x => x);

        List<Guid> entryInfoIds = ftsUnion
            .Where(x => x.EntryInfoId.HasValue)
            .Select(x => x.EntryInfoId!.Value)
            .ToList();
        
        var entryInfos = db.EntryInfos.Where(x => entryInfoIds.Contains(x.Id)).ToDictionary(x => x.Id, x => x);
        foreach (var item in ftsUnion)
        {
            // item.TempId = Guid.NewGuid();
            var entityName = "???";
            switch (item.FtsEntityName)
            {
                case nameof(FtsEntry):
                    ArgumentNullException.ThrowIfNull(item.EntryId);
                    entityName = entries[item.EntryId.Value].Discriminator;
                    break;
                case nameof(FtsEntryInfo):
                    ArgumentNullException.ThrowIfNull(item.EntryInfoId);
                    entityName = entryInfos[item.EntryInfoId.Value].Discriminator;
                    break;
            }

            item.EntityName = entityName;
        }

        return ftsUnion;
    }
}
using Relaks.Models;
using Relaks.Models.Misc;
using Relaks.Models.Requests;
using Relaks.Utils.Extensions;

namespace Relaks.Database.Repositories;

public static class EntryRepository
{
    public static PaginatableResult<BaseEntry> FindByReq(this IQueryable<BaseEntry> q, EntryFilterRequest req)
    {
        if (!string.IsNullOrEmpty(req.Discriminator))
            q = q.Where(x => x.Discriminator.Equals(req.Discriminator));

        q = string.IsNullOrEmpty(req.OrderBy) ? q.OrderByDescending(x => x.UpdatedAt) : q.OrderBy(req);

        return q.Paginate(req);
    }

    public static List<FtsEntry> FtsEntrySearch(this AppDbContext db, string search, string? discriminator)
    {
        if (string.IsNullOrEmpty(search)) return new List<FtsEntry>();

        var s = $"\"{search}\"*";

        var ftsQuery = db.Set<FtsEntry>()
            .Where(x => x.Match == s)
            .Select(x => new FtsEntry()
            {
                Id = x.Id,
                Rank = x.Rank,
                Snippet = db.Snippet(x.Match, "-1", "<b>", "</b>", "...", 5),
            })
            .OrderByDescending(x => x.Rank)
            .AsEnumerable()
            ;

        var ftsEntries = ftsQuery.Take(50).ToList();

        var entryIds = ftsEntries.Select(x => x.Id).ToList();
        var entries = db.BaseEntries.Where(x => entryIds.Contains(x.Id)).ToDictionary(x => x.Id, x => x);
        ftsEntries.ForEach(x =>
        {
            x.BaseEntry = entries[x.Id];
        });
        
        if (!string.IsNullOrEmpty(discriminator))
        {
            ftsEntries = ftsEntries.Where(x => x.BaseEntry != null && x.BaseEntry.Discriminator.Equals(discriminator)).ToList();
        }

        return ftsEntries.Take(15).ToList();
    }
}
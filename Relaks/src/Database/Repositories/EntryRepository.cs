using Relaks.Models;
using Relaks.Models.Misc;
using Relaks.Models.Requests;
using Relaks.Utils.Extensions;

namespace Relaks.Database.Repositories;

public static class EntryRepository
{
    public static PaginatableResult<BaseEntry> FindByReq(this IQueryable<BaseEntry> q, EntryFilterRequest req)
    {
        q = req.IsDeleted == true ? q.Where(x => x.DeletedAt != null) : q.Where(x => x.DeletedAt == null);
        
        if (!string.IsNullOrEmpty(req.Discriminator))
            q = q.Where(x => x.Discriminator.Equals(req.Discriminator));

        q = string.IsNullOrEmpty(req.OrderBy) ? q.OrderByDescending(x => x.UpdatedAt) : q.OrderBy(req);

        return q.Paginate(req);
    }

    public static List<FtsEntry> FtsEntrySearch(this AppDbContext db, string search, EntryFilterRequest req)
    {
        if (string.IsNullOrEmpty(search)) return new List<FtsEntry>();

        var s = $"\"{search}\"*";

        var q = db.Set<FtsEntry>().Where(x => x.Match == s);
        
        q = req.IsDeleted == true
            ? q.Where(x => !string.IsNullOrEmpty(x.DeletedAt))
            : q.Where(x => string.IsNullOrEmpty(x.DeletedAt));

        if (!string.IsNullOrEmpty(req.Discriminator))
        {
            q = q.Where(x => x.Discriminator.Equals(req.Discriminator));
        }

        var ftsEntries = q
            .Select(x => new FtsEntry()
            {
                Id = x.Id,
                Rank = x.Rank,
                Snippet = db.Snippet(x.Match, "-1", "<b>", "</b>", "...", 5),
                Discriminator = x.Discriminator,
                DeletedAt = x.DeletedAt,
            })
            .OrderByDescending(x => x.Rank)
            .AsEnumerable()
            .Take(15)
            .ToList()
            ;

        var entryIds = ftsEntries.Select(x => x.Id).ToList();
        var entries = db.BaseEntries.Where(x => entryIds.Contains(x.Id)).ToDictionary(x => x.Id, x => x);
        ftsEntries.ForEach(x =>
        {
            x.BaseEntry = entries[x.Id];
        });

        return ftsEntries;
    }
}
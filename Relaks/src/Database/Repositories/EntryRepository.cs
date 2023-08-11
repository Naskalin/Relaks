using Microsoft.EntityFrameworkCore;
using Relaks.Interfaces;
using Relaks.Models;
using Relaks.Models.Misc;
using Relaks.Utils.Extensions;

namespace Relaks.Database.Repositories;

public class EntryFindRequest : IPaginatable, IOrderable
{
    public int Page { get; set; }
    public int PerPage { get; set; }
    public string? Discriminator { get; set; }
    public string? OrderBy { get; set; }
    public bool? IsOrderByDesc { get; set; }
    public bool? IsDeleted { get; set; }
    public string? Search { get; set; }
    public List<Guid> ProfessionIds { get; set; } = new();
}

public class EntryFindResult
{
    public BaseEntry BaseEntry { get; set; } = null!;
    public FtsEntry? FtsEntry { get; set; }
}

public static class EntryRepository
{
    // public static IQueryable<BaseEntry> FindByIdQuery(this IQueryable<BaseEntry> q, Guid entryId)
    // {
    //     return q
    //             .Where(x => x.Id.Equals(entryId))
    //             .Include(x => x.EntryInfos)
    //         ;
    // }

    public static PaginatableResult<EntryFindResult> FindEntries(this AppDbContext db, EntryFindRequest req)
    {
        if (!string.IsNullOrEmpty(req.Search)) return FtsEntrySearch(db, req);

        var q = db.BaseEntries
            .Include(x => x.Professions)
            .AsQueryable();
        q = req.IsDeleted == true ? q.Where(x => x.DeletedAt != null) : q.Where(x => x.DeletedAt == null);

        if (req.ProfessionIds.Any())
        {
            q = q.Where(x => x.Professions.Any(prof => req.ProfessionIds.Contains(prof.Id)));
        }

        if (!string.IsNullOrEmpty(req.Discriminator))
            q = q.Where(x => x.Discriminator.Equals(req.Discriminator));

        q = string.IsNullOrEmpty(req.OrderBy) ? q.OrderByDescending(x => x.UpdatedAt) : q.OrderBy(req);

        var paginated = q.ToPaginatedResult(req);

        return new PaginatableResult<EntryFindResult>
        {
            Page = paginated.Page,
            PerPage = paginated.PerPage,
            PageCount = paginated.PageCount,
            Total = paginated.Total,
            Items = paginated.Items.Select(x => new EntryFindResult {BaseEntry = x}).ToList()
        };
    }

    private static PaginatableResult<EntryFindResult> FtsEntrySearch(this AppDbContext db, EntryFindRequest req)
    {
        if (string.IsNullOrEmpty(req.Search)) return new PaginatableResult<EntryFindResult>();

        var s = $"\"{req.Search}\"*";

        var q = db.Set<FtsEntry>().Where(x => x.Match == s);

        q = req.IsDeleted == true
            ? q.Where(x => !string.IsNullOrEmpty(x.DeletedAt))
            : q.Where(x => string.IsNullOrEmpty(x.DeletedAt));

        if (!string.IsNullOrEmpty(req.Discriminator))
        {
            q = q.Where(x => x.Discriminator.Equals(req.Discriminator));
        }

        q = q.Select(x => new FtsEntry()
                {
                    Id = x.Id,
                    Rank = x.Rank,
                    Snippet = db.Snippet(x.Match, "-1", "<b>", "</b>", "...", 5),
                    Discriminator = x.Discriminator,
                    DeletedAt = x.DeletedAt,
                })
                .OrderByDescending(x => x.Rank)
            ;

        var paginated = q.ToPaginatedResult(req);

        var entryIds = paginated.Items.Select(x => x.Id).ToList();
        var entries = db.BaseEntries.Where(x => entryIds.Contains(x.Id)).ToDictionary(x => x.Id, x => x);
        return new PaginatableResult<EntryFindResult>
        {
            Page = paginated.Page,
            PerPage = paginated.PerPage,
            PageCount = paginated.PageCount,
            Total = paginated.Total,
            Items = paginated.Items.Select(x => new EntryFindResult {BaseEntry = entries[x.Id], FtsEntry = x,}).ToList()
        };
    }
}
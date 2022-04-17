using System.Text.Json;
using App.DbConfigurations;
using App.Endpoints.Entries;
using App.Models;

namespace App.Repository;

public class EntryFtsDto : Entry
{
    public string? Snippet { get; set; }
}

public class EntryRepository : BaseRepository<Entry>
{
    public EntryRepository(AppDbContext db) : base(db)
    {
    }

    public IQueryable<FtsResult> FindFtsResults(string? search)
    {
        return from entryFts in Db.Set<EntryFts>()
            where entryFts.Match == search + "*"
            orderby entryFts.Rank
            select new FtsResult()
            {
                Id = entryFts.Id,
                Snippet = Db.Snippet(entryFts.Match, "-1", "<b>", "</b>", "...", 10),
            };
    }

    private static EntryFtsDto CreateDto(Entry entry, string snippet)
    {
        var dto = JsonSerializer.Deserialize<EntryFtsDto>(JsonSerializer.Serialize(entry));
        if (dto == null) throw new ArgumentException("EntryFtsResultDto, deserialize error");
        dto.Snippet = snippet;

        return dto;
    }

    public IEnumerable<EntryFtsDto> FindByListRequest(ListRequest req)
    {
        IEnumerable<EntryFtsDto>? query;
        // var query = Db.Set<EntryFts>().AsQueryable();
        if (String.IsNullOrEmpty(req.Search))
        {
            // var entries = Db.Entries.AsEnumerable();

            query = Db.Entries.AsEnumerable().Select(x => CreateDto(x, ""));
            // query = Db.Entries
            //     .Where(x => true)
            //     .Select(x => CreateDto(x, ""));

            // query = entries.Select(x => CreateDto(x, ""));
            // .AsEnumerable()
            // .Select(x => CreateDto(x, ""));

            // query = entries.Select(x => CreateDto(Db, x, null));

            // if (!string.IsNullOrEmpty(req.OrderBy))
            //     query = query.OrderBy(req.OrderBy, req.OrderByDesc ?? false);
            // else
            query = query.OrderByDescending(x => x.UpdatedAt);
        }
        else
        {
            query = Db.Set<EntryFts>()
                .Where(x => x.Match == req.Search + "*")
                .OrderBy(x => x.Rank)
                .Join(
                    Db.Entries,
                    x => x.Id,
                    e => e.Id,
                    (fts, entry) => CreateDto(entry, Db.Snippet(fts.Match, "-1", "<b>", "</b>", "...", 10))
                );
        }

        if (req.isDeleted != null)
            query = query.Where(x => req.isDeleted == true ? x.DeletedAt != null : x.DeletedAt == null);

        if (req.EntryType != null)
            query = query.Where(x => x.EntryType == req.EntryType);

        var perPage = req.PerPage ?? 50;
        var page = req.Page ?? 1;

        return query
            .Skip(perPage * (page - 1))
            .Take(perPage);
    }
}
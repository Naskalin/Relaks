using System.Text.Json;
using App.DbConfigurations;
using App.Endpoints.Entries;
using App.Models;

namespace App.Repository;

public class EntryFtsDto
{
    public Guid EntryId { get; set; }
    public string Snippet { get; set; } = null!;
    public double Rank { get; set; }
}

public class EntryDto : Entry 
{
    public string? Snippet { get; set; }
    public double? Rank { get; set; }
}

public class EntryRepository : BaseRepository<Entry>
{
    public EntryRepository(AppDbContext db) : base(db)
    {
    }

    private static EntryDto CreateEntryDto(Entry entry, string? snippet = null, double? rank = null)
    {
        var dto = JsonSerializer.Deserialize<EntryDto>(JsonSerializer.Serialize(entry));
        if (dto == null) throw new ArgumentException("EntryFtsResultDto, deserialize error");
        if (snippet != null)
            dto.Snippet = snippet;
        if (rank != null)
            dto.Rank = rank;

        return dto;
    }

    public IEnumerable<EntryDto> FindByListRequest(ListRequest req)
    {
        IEnumerable<EntryDto> query;
        
        if (String.IsNullOrEmpty(req.Search))
        {
            query = Db.Entries.AsEnumerable().Select(x => CreateEntryDto(x));
            query = query.OrderByDescending(x => x.UpdatedAt);
        }
        else
        {
            query = FtsSearch(req.Search);
        }
    
        if (req.IsDeleted != null)
            query = query.Where(x => req.IsDeleted == true ? x.DeletedAt != null : x.DeletedAt == null);
    
        if (req.EntryType != null)
            query = query.Where(x => x.EntryType == req.EntryType);
    
        var perPage = req.PerPage ?? 50;
        var page = req.Page ?? 1;
        query = query
            .Skip(perPage * (page - 1))
            .Take(perPage);
    
        return query;
    }

    public IEnumerable<EntryDto> FtsSearch(string search)
    {
        var entryFtsPriority = 10;
        var s = $"\"{search}\"*";
        
        var fts1 = Db.Set<FtsEntry>()
                .Where(x => x.Match == s)
                .Select(x => new EntryFtsDto()
                {
                    EntryId = x.Id,
                    Rank = entryFtsPriority * (double) x.Rank!,
                    Snippet = Db.Snippet(x.Match, "-1", "<b>", "</b>", "...", 5)
                })
            ;
        
        var fts2 = Db.Set<FtsEntryInfo>()
                .Where(x => x.Match == s)
                .Select(x => new EntryFtsDto()
                {
                    EntryId = x.EntryId,
                    Rank = (double) x.Rank!,
                    Snippet = Db.Snippet(x.Match, "-1", "<b>", "</b>", "...", 5)
                })
            ;

        var ftsUnion = fts1
                .AsEnumerable()
                .Union(fts2)
                .GroupBy(x => x.EntryId)
                .Select(items => items.OrderBy(x => x.Rank).First())
                .OrderBy(x => x.Rank)
                .Join(
                    Db.Entries,
                    x => x.EntryId,
                    e => e.Id,
                    (entryFts, entry) => CreateEntryDto(entry, entryFts.Snippet, entryFts.Rank)
                )
            ;
        return ftsUnion;
    }
}
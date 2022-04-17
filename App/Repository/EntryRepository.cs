using System.Text.Json;
using App.DbConfigurations;
using App.Endpoints.Entries;
using App.Models;
using App.Utils.Extensions.Database;
using Microsoft.EntityFrameworkCore.Internal;

namespace App.Repository;

public interface IFtsDtoResult
{
    public string Snippet { get; set; }
    public double Rank { get; set; }
}

public class EntryFtsDto : IFtsDtoResult
{
    public Guid EntryId { get; set; }
    public string Snippet { get; set; } = null!;
    public double Rank { get; set; }
}

// public class EntryInfoFtsDto : IFtsDtoResult
// {
//     public string EntryId { get; set; } = null!;
//     public string Snippet { get; set; } = null!;
//     public double Rank { get; set; }
// }

// public class FtsDtoResult : IFtsDtoResult
// {
//     public string? Snippet { get; set; }
//     public double? Rank { get; set; }
// }
//
// public class EntryFtsDto : Entry, IFtsDtoResult
// {
//     public string? Snippet { get; set; }
//     public double? Rank { get; set; }
// }
//
// public class EntryInfoFtsDto : IFtsDtoResult
// {
//     public Guid Id { get; set; }
//     public Guid EntryId { get; set; }
//     public string? Snippet { get; set; }
//     public double? Rank { get; set; }
// }


public class EntryRepository : BaseRepository<Entry>
{
    public EntryRepository(AppDbContext db) : base(db)
    {
    }

    public IQueryable<FtsResult> FindFtsResults(string? search)
    {
        return Db.Set<EntryFts>()
            .Where(entryFts => entryFts.Match == search + "*")
            .OrderBy(entryFts => entryFts.Rank)
            .Select(entryFts => new FtsResult()
            {
                Id = entryFts.Id, 
                Snippet = Db.Snippet(entryFts.Match, "-1", "<b>", "</b>", "...", 10),
            });
    }

    // private static EntryFtsDto CreateDto(Entry entry, IFtsDtoResult ftsDto)
    // {
    //     var dto = JsonSerializer.Deserialize<EntryFtsDto>(JsonSerializer.Serialize(entry));
    //     if (dto == null) throw new ArgumentException("EntryFtsResultDto, deserialize error");
    //     dto.Snippet = ftsDto.Snippet;
    //     dto.Rank = ftsDto.Rank;
    //
    //     return dto;
    // }

    // public IEnumerable<EntryFtsDto> FindByListRequest(ListRequest req)
    // {
    //     IEnumerable<EntryFtsDto> query;
    //     if (String.IsNullOrEmpty(req.Search))
    //     {
    //         query = Db.Entries.AsEnumerable().Select(x => CreateDto(x, new FtsDtoResult()));
    //         query = query.OrderByDescending(x => x.UpdatedAt);
    //     }
    //     else
    //     {
    //         query = FtsSearch(req.Search);
    //     }
    //
    //     if (req.isDeleted != null)
    //         query = query.Where(x => req.isDeleted == true ? x.DeletedAt != null : x.DeletedAt == null);
    //
    //     if (req.EntryType != null)
    //         query = query.Where(x => x.EntryType == req.EntryType);
    //
    //     // var perPage = req.PerPage ?? 50;
    //     // var page = req.Page ?? 1;
    //     // query = query
    //     //     .Skip(perPage * (page - 1))
    //     //     .Take(perPage);
    //
    //     return query;
    // }

    // public IEnumerable<IGrouping<Guid, EntryFtsDto>> FtsSearch(string search)
    public IEnumerable<EntryFtsDto> FtsSearch(string search)
    // public IEnumerable<EntryFtsDto> FtsSearch(string search)
    {
        var s = $"\"{search}\"*";
        
        var fts1 = Db.Set<EntryFts>()
                .Where(x => x.Match == s)
                // .OrderBy(x => x.Rank)
                .Select(x => new EntryFtsDto()
                {
                    EntryId = x.Id,
                    Rank = (double) x.Rank,
                    Snippet = Db.Snippet(x.Match, "-1", "<b>", "</b>", "...", 10)
                })
            ;
        
        var fts2 = Db.Set<EntryInfoFts>()
                .Where(x => x.Match == s)
                // .OrderBy(x => x.Rank)
                .Select(x => new EntryFtsDto()
                {
                    EntryId = x.EntryId,
                    Rank = (double) x.Rank,
                    Snippet = Db.Snippet(x.Match, "-1", "<b>", "</b>", "...", 10)
                })
            ;

        var ftsUnion = fts1
            .AsEnumerable()
            .Union(fts2)
            .GroupBy(x => x.EntryId)
            ;
        // var entriesDto2 = searchEntryInfos.Join(
        //     Db.Entries,
        //     x => x.EntryId,
        //     e => e.Id,
        //     (entryInfoFtsDto, entry) => CreateDto(entry, new FtsDtoResult()
        //     {
        //         Snippet = entryInfoFtsDto.Snippet,
        //         Rank = entryInfoFtsDto.Rank
        //     })
        // );
        //
        // var union = entriesDto
        //     .AsEnumerable()
        //     .Union(entriesDto2)
        //     .GroupBy(x => x.Id)
        //     .Select(dtos => dtos.OrderBy(x => x.Rank).First())
        //     .SelectMany(dtos => new
        //     {
        //         Snippets = dtos.Select(x => x)
        //     })
        //     // .OrderBy(x => x.Rank)
        //     // .Select(dtos => dtos.OrderBy(x => x.Rank).First())
        //     // .OrderBy(x => x.Rank)
        //     ;
        //
        return union;
    }
}
using System.Collections;
using Microsoft.EntityFrameworkCore.Internal;
using Relaks.Models;

namespace Relaks.Database.Repostitories;

public static class FtsRepository
{
    // public class EntryFtsDto
    // {
    //     public Guid EntryId { get; set; }
    //     public string Snippet { get; set; } = null!;
    //     public double Rank { get; set; }
    // }
    //
    // public class EntryDto : BaseEntry 
    // {
    //     public string? Snippet { get; set; }
    //     public double? Rank { get; set; }
    // }
    
    public interface IFtsSearch
    {
        public double? Rank { get; set; }
    }

    public class FtsSearchEntity<TEntity> : IComparable, IFtsSearch
    {
        public string Snippet { get; set; } = null!;
        public double? Rank { get; set; }
        public TEntity Entity { get; set; } = default!;
        
        public int CompareTo(object? obj)
        {
            ArgumentNullException.ThrowIfNull(obj);
            var e = (FtsSearchEntity<TEntity>) obj;
            var rank = Rank ?? 0;
            return rank.CompareTo(e?.Rank);
        }
    }

    public class FtsResult
    {
        public List<FtsSearchEntity<BaseEntry>> Entries { get; set; } = new();
    
        public bool HasAny()
        {
            return Entries.Any();
        }
    }

    // private static EntryDto CreateEntryDto(Entry entry, string? snippet = null, double? rank = null)
    // {
    //     var dto = JsonSerializer.Deserialize<EntryDto>(JsonSerializer.Serialize(entry));
    //     if (dto == null) throw new ArgumentException("EntryFtsResultDto, deserialize error");
    //     if (snippet != null)
    //         dto.Snippet = snippet;
    //     if (rank != null)
    //         dto.Rank = rank;
    //
    //     return dto;
    // }

    public static ArrayList FtsSearch(this AppDbContext db, string search)
    {
        var s = $"\"{search}\"*";
        var entries = db.Set<FtsEntry>()
            .Where(x => x.Match == s)
            // .OrderBy(x => x.Rank)
            // .Take(5)
            // .Join(
            //     db.Entries,
            //     fts => fts.Id,
            //     e => e.Id,
            //     (fts, e) => new FtsSearchEntity<BaseEntry>()
            //     {
            //         Entity = e,
            //         Rank = fts.Rank,
            //         Snippet = db.Snippet(fts.Match, "-1", "<b>", "</b>", "...", 5)
            //     }
            // )
            .ToList();
        ;

        // var entryInfos = db.Set<FtsEntryInfo>()
        //         .Where(x => x.Match == s)
        //         .OrderBy(x => x.Rank)
        //         .Take(5)
        //         .Join(
        //             db.EntryInfos,
        //             fts => fts.Id,
        //             e => e.Id,
        //             (fts, e) => new FtsSearchEntity<BaseEntryInfo>()
        //             {
        //                 Entity = e,
        //                 Rank = fts.Rank,
        //                 Snippet = db.Snippet(fts.Match, "-1", "<b>", "</b>", "...", 5)
        //             }
        //         )
        //         .ToList();
        //     ;
            
        var arr = new ArrayList();
        arr.AddRange(entries);
        // arr.AddRange(entryInfos);
        
        // arr.Sort();
        return arr;
        
        // return new FtsResult()
        // {
        //     Entries = entries
        // };
        // .Select(x =>
        // {
        //     
        // })
        // .Select(x => new EntryFtsDto()
        // {
        //     EntryId = x.Id,
        //     Rank = entryFtsPriority * (double) x.Rank!,
        //     Snippet = db.Snippet(x.Match, "-1", "<b>", "</b>", "...", 5)
        // })
        ;

        // var fts2 = Db.Set<FtsEntryInfo>()
        //         .Where(x => x.Match == s)
        //         .Select(x => new EntryFtsDto()
        //         {
        //             EntryId = x.EntryId,
        //             Rank = (double) x.Rank!,
        //             Snippet = Db.Snippet(x.Match, "-1", "<b>", "</b>", "...", 5)
        //         })
        //     ;
        //
        // var ftsUnion = fts1
        //         .AsEnumerable()
        //         .Union(fts2)
        //         .GroupBy(x => x.EntryId)
        //         .Select(items => items.OrderBy(x => x.Rank).First())
        //         .OrderBy(x => x.Rank)
        //         .Join(
        //             Db.Entries,
        //             x => x.EntryId,
        //             e => e.Id,
        //             (entryFts, entry) => CreateEntryDto(entry, entryFts.Snippet, entryFts.Rank)
        //         )
        //     ;
        // return ftsUnion;
    }
}
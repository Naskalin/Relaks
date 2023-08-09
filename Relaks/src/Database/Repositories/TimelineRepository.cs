
using Microsoft.EntityFrameworkCore;
using Relaks.DataHelpers;
using Relaks.Mappers;
using Relaks.Models;

namespace Relaks.Database.Repositories;

public class TimelineItem
{
    public DateTime Date { get; set; }
    public string? KeywordTitle { get; set; }
    public bool WithTime { get; set; }
    public string EntityName { get; set; } = null!;
    public object Entity { get; set; } = null!;
}

public class TimelineRequest
{
    public Dictionary<string, List<string>> DiscriminatorProperties { get; set; } = new();

    public DateTime StartDay { get; set; }
    public DateTime EndDay { get; set; }
    public List<Guid> EntryIds { get; set; } = new();
}

public static class TimelineRepository
{
    public static IReadOnlyList<TimelineItem> FindTimeline(this AppDbContext db, TimelineRequest req)
    {
        var sDay = req.StartDay.Day;
        var sMonth = req.StartDay.Month;
        var eDay = req.EndDay.Day;
        var eMonth = req.EndDay.Month;
        var result = new List<TimelineItem>();
        var reqEntryDiscriminatorsDict = req.DiscriminatorProperties
                .Where(x => DataHelper.EntryDiscriminators.Contains(x.Key))
                .ToDictionary(x => x.Key, x => x.Value)
            ;
        var entriesQuery = db.BaseEntries.AsQueryable();
        if (req.EntryIds.Any())
        {
            entriesQuery = entriesQuery.Where(x => req.EntryIds.Contains(x.Id));
        }

        var discriminatorStarts = reqEntryDiscriminatorsDict
                .Where(x => x.Value.Contains(nameof(BaseEntry.StartAt)))
                .Select(x => x.Key)
                .ToArray()
            ;
        if (!req.DiscriminatorProperties.Any() || discriminatorStarts.Any())
        {
            var queryEntryStarts = entriesQuery
                    .Where(x => x.StartAt.HasValue
                                && x.StartAt.Value.Day >= sDay
                                && x.StartAt.Value.Month >= sMonth
                                && x.StartAt.Value.Day <= eDay
                                && x.StartAt.Value.Month <= eMonth)
                ;

            if (discriminatorStarts.Any())
            {
                queryEntryStarts = queryEntryStarts.Where(x => discriminatorStarts.Contains(x.Discriminator));   
            }
            
            result.AddRange(queryEntryStarts.Select(x => new TimelineItem()
            {
                Date = x.StartAt!.Value,
                EntityName = x.Discriminator,
                Entity = x,
                WithTime = x.StartAtWithTime,
                KeywordTitle = Resources.Entity.ResourceManager.GetString($"{x.Discriminator}_StartAt")
            }));   
        }

        var discriminatorEnds = reqEntryDiscriminatorsDict
                .Where(x => x.Value.Contains(nameof(BaseEntry.EndAt)))
                .Select(x => x.Key)
                .ToArray()
            ;
        if (!req.DiscriminatorProperties.Any() || discriminatorEnds.Any())
        {
            var queryEntryEnds = entriesQuery
                    .Where(x => x.EndAt.HasValue 
                                && x.EndAt.Value.Day >= sDay 
                                && x.EndAt.Value.Month >= sMonth 
                                && x.EndAt.Value.Day <= eDay 
                                && x.EndAt.Value.Month <= eMonth)
                ;

            if (discriminatorEnds.Any())
            {
                queryEntryEnds = queryEntryEnds.Where(x => discriminatorEnds.Contains(x.Discriminator));   
            }

            result.AddRange(queryEntryEnds.Select(x => new TimelineItem()
            {
                Date = x.EndAt!.Value,
                EntityName = x.Discriminator,
                Entity = x,
                WithTime = x.EndAtWithTime,
                KeywordTitle = Resources.Entity.ResourceManager.GetString($"{x.Discriminator}_EndAt")
            }));   
        }

        if (!req.DiscriminatorProperties.Any() || req.DiscriminatorProperties.ContainsKey(nameof(EiDate)))
        {
            var queryEiDates = db.EiDates
                    .Include(x => x.Entry)
                    .Where(x => x.Date.Day >= sDay 
                                && x.Date.Month >= sMonth 
                                && x.Date.Day <= eDay 
                                && x.Date.Month <= eMonth)
                ;
            
            if (req.EntryIds.Any())
            {
                queryEiDates = queryEiDates.Where(x => req.EntryIds.Contains(x.Entry.Id));
            }
            
            result.AddRange(queryEiDates.Select(x => x.ToTimelineItem()));
        }

        return result
            .OrderBy(x => x.Date.Day)
            .ThenBy(x => x.Date.Hour)
            .ThenBy(x => x.Date.Minute)
            .ToArray();
    }
}
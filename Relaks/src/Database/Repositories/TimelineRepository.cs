
using Microsoft.EntityFrameworkCore;
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
}

public static class TimelineRepository
{
    public static List<TimelineItem> FindTimeline(this AppDbContext db, TimelineRequest req)
    {
        var sDay = req.StartDay.Day;
        var sMonth = req.StartDay.Month;
        var eDay = req.EndDay.Day;
        var eMonth = req.EndDay.Month;

        var queryEntryStarts = db.BaseEntries
            .Where(x => x.StartAt.HasValue
                        && x.StartAt.Value.Day >= sDay
                        && x.StartAt.Value.Month >= sMonth
                        && x.StartAt.Value.Day <= eDay
                        && x.StartAt.Value.Month <= eMonth)
            .Select(x => new TimelineItem()
            {
                Date = x.StartAt!.Value,
                EntityName = x.Discriminator,
                Entity = x,
                WithTime = x.StartAtWithTime,
                KeywordTitle = Resources.Entity.ResourceManager.GetString($"{x.Discriminator}_StartAt")
            });

        var queryEntryEnds = db.BaseEntries
            .Where(x => x.EndAt.HasValue 
                        && x.EndAt.Value.Day >= sDay 
                        && x.EndAt.Value.Month >= sMonth 
                        && x.EndAt.Value.Day <= eDay 
                        && x.EndAt.Value.Month <= eMonth)
            .Select(x => new TimelineItem()
            {
                Date = x.EndAt!.Value,
                EntityName = x.Discriminator,
                Entity = x,
                WithTime = x.EndAtWithTime,
                KeywordTitle = Resources.Entity.ResourceManager.GetString($"{x.Discriminator}_EndAt")
            });

        var eiDates = db.EiDates;
        if (!req.DiscriminatorProperties.Any() || req.DiscriminatorProperties.ContainsKey(nameof(EiDate)))
        {
            
        }

        var queryEiDates = db.EiDates
            .Include(x => x.Entry)
            .Where(x => x.Date.Day >= sDay 
                        && x.Date.Month >= sMonth 
                        && x.Date.Day <= eDay 
                        && x.Date.Month <= eMonth)
            .Select(x => x.ToTimelineItem());

        return queryEiDates
            .AsEnumerable()
            .Union(queryEntryStarts)
            .Union(queryEntryEnds)
            .OrderBy(x => x.Date.Day)
            .ToList();
    }
}
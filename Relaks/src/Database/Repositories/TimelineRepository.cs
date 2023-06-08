
using Microsoft.EntityFrameworkCore;

namespace Relaks.Database.Repositories;

public class TimelineItem
{
    public DateTime Date { get; set; }
    public string? DateType { get; set; }
    public bool WithTime { get; set; }
    public string EntityName { get; set; } = null!;
    public object Entity { get; set; } = null!;
}

public class TimelineRequest
{
    public bool IsStarts { get; set; }
    public bool IsEnds { get; set; }
    public string? Discriminator { get; set; }
    
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
                DateType = Resources.Entity.ResourceManager.GetString(string.Format(x.Discriminator+"_StartAt"))
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
                DateType = Resources.Entity.ResourceManager.GetString(string.Format(x.Discriminator+"_EndAt"))
            });
        
        var queryEiDates = db.EiDates
            .Include(x => x.Entry)
            .Where(x => x.Date.Day >= sDay 
                        && x.Date.Month >= sMonth 
                        && x.Date.Day <= eDay 
                        && x.Date.Month <= eMonth)
            .Select(x => new TimelineItem()
        {
            Date = x.Date,
            EntityName = x.Discriminator,
            Entity = x,
            WithTime = x.WithTime,
        });

        return queryEiDates
            .AsEnumerable()
            .Union(queryEntryStarts)
            .Union(queryEntryEnds)
            .OrderBy(x => x.Date.Day)
            .ToList();
    }
}
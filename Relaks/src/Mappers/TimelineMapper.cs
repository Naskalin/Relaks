using System.Linq.Expressions;
using FluentValidation.Internal;
using Relaks.Database.Repositories;
using Relaks.Models;
using YamlDotNet.Helpers;

namespace Relaks.Mappers;

public static class TimelineMapper
{
    public static TimelineItem ToTimelineItem(this EiDate eiDate) => new()
    {
        Date = eiDate.Date,
        EntityName = eiDate.Discriminator,
        Entity = eiDate,
        WithTime = eiDate.WithTime,
    };

    // public static TimelineItem ToTimelineItem(this BaseEntry baseEntry, Expression<Func<BaseEntry, DateTime?>> expr)
    // {
    //     // ArgumentNullException.ThrowIfNull(expr.Name);
    //     // var date = baseEntry.GetType().GetProperty(expr.Name)?.GetValue(baseEntry);
    //     // ArgumentNullException.ThrowIfNull(date);
    //     
    //     return new TimelineItem()
    //     {
    //         // Date = (DateTime) date,
    //         Date = new DateTime(),
    //         EntityName = baseEntry.Discriminator,
    //         Entity = baseEntry,
    //         WithTime = baseEntry.EndAtWithTime,
    //         // KeywordTitle
    //             // = Resources.Entity.ResourceManager.GetString($"{baseEntry.Discriminator}_{expr.Name}")
    //     };
    // }
}
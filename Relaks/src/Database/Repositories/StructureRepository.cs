using Microsoft.EntityFrameworkCore;
using Relaks.Models.StructureModels;
using Relaks.Utils.Extensions;

namespace Relaks.Database.Repositories;

public class StructureGroupListRequest
{
    public Guid EntryId { get; set; }
    public bool IsEnableDate { get; set; } = true;
    public DateTime Date { get; set; } = DateTime.Now.StartOfDay();
}

public static class StructureRepository
{
    public static List<StructureGroup> ToTree(this IQueryable<StructureGroup> query, StructureGroupListRequest req)
    {
        query = query.Where(x => x.EntryId.Equals(req.EntryId));

        if (req.IsEnableDate)
        {
            query = query
                .Include(x => x.Items.Where(item => req.Date >= item.StartAt && (item.EndAt == null || req.Date <= item.EndAt))).ThenInclude(x => x.Entry)
                .Where(x => x.StartAt <= req.Date && (x.EndAt == null || req.Date <= x.EndAt));
        }
        else
        {
            query = query.Include(x => x.Items).ThenInclude(x => x.Entry);
        }
        return query.ToBaseTree();
    }
    
    // public static List<(string Title, Guid Value, int Level)> ToTreeSelect(this List<StructureGroup> tree,
    //     List<(string Title, Guid Value, int Level)>? result = null, int? level = null)
    // {
    //     result ??= new();
    //     level = level.HasValue ? level.Value + 1 : 0;
    //
    //     var dashes = new List<string>();
    //     for (int i = 0; i < level.Value; i++)
    //     {
    //         dashes.Add("—");
    //     }
    //
    //     var beforeTitle = string.Join(" ", dashes);
    //     if (dashes.Any()) beforeTitle += " ";
    //
    //     foreach (var category in tree)
    //     {
    //         result.Add((beforeTitle + category.Title, category.Id, level.Value));
    //         category.Children.ToTreeSelect(result, level);
    //     }
    //
    //     return result;
    // }
}
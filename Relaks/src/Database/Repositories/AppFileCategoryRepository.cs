using BootstrapBlazor.Components;
using Microsoft.EntityFrameworkCore;
using Relaks.Models;

namespace Relaks.Database.Repositories;

public static class AppFileCategoryRepository
{
    public static List<BaseFileCategory> ToTree(this IQueryable<BaseFileCategory> q)
    {
        var all = q
            .Include(x => x.Children)
            .AsEnumerable()
            .OrderBy(x => x.Title, StringComparer.OrdinalIgnoreCase)
            .ToList();
        
        return all.Where(c => c.ParentId.Equals(null)).ToList();
    }

    public static List<(string Title, Guid Value, int Level)> ToTreeSelect(this List<BaseFileCategory> tree, List<(string Title, Guid Value, int Level)>? result = null, int? level = null)
    {
        result ??= new();
        level = level.HasValue ? level.Value + 1 : 0;
        
        var dashes = new List<string>();
        for (int i = 0; i < level.Value; i++)
        {
            dashes.Add("—");   
        }

        var beforeTitle = string.Join(" ", dashes);
        if (dashes.Any()) beforeTitle += " ";
        
        foreach (var category in tree)
        {
            result.Add(( beforeTitle + category.Title, category.Id, level.Value));
            category.Children.ToTreeSelect(result, level);
        }

        return result;
    }
}
using Microsoft.EntityFrameworkCore;
using Relaks.Interfaces;

namespace Relaks.Database.Repositories;

public static class TreeRepository
{
    public static List<TEntity> ToBaseTree<TEntity>(this IQueryable<TEntity> q) where TEntity : class, ITree<TEntity>
    {
        var all = q
            .Include(x => x.Children)
            .AsEnumerable()
            .OrderBy(x => x.Title, StringComparer.OrdinalIgnoreCase)
            .ToList();
    
        return all.Where(c => c.ParentId.Equals(null)).ToList();
    }
    
    // public static List<(string Title, Guid Value, int Level)> ToTreeSelect<TEntity>(
    //     this List<ITree<TEntity>> tree,
    //     List<(string Title, Guid Value, int Level)>? result = null, int? level = null)
    //     where TEntity : class, ITree<TEntity>
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
    //         category.Children.ToTreeSelect<TEntity>(result, level);
    //     }
    //
    //     return result;
    // }
}
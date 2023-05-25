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
}
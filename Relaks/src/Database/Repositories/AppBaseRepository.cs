using Relaks.Interfaces;
using Relaks.Models.Misc;

namespace Relaks.Database.Repositories;

public static class AppBaseRepository
{
    public static PaginatableResult<TEntity> Paginate<TEntity>(this IQueryable<TEntity> query, IPaginatable paginatable)
    {
        var perPage = paginatable.PerPage ?? 25;
        var page = paginatable.Page ?? 1;

        if (perPage > 300) perPage = 300;

        paginatable.Page = perPage;
        paginatable.PerPage = page;

        return new PaginatableResult<TEntity>()
        {
            Items = query.Skip(perPage * (page - 1)).Take(perPage).ToList(),
            Page = page,
            PerPage = perPage,
            Total = query.Count()
        };
    }
}
﻿using Relaks.Interfaces;
using Relaks.Models.Misc;

namespace Relaks.Database.Repositories;

public static class AppBaseRepository
{
    public static PaginatableResult<TEntity> Paginate<TEntity>(this IQueryable<TEntity> query, IPaginatable paginatable)
    {
        var perPage = paginatable.PerPage;
        var page = paginatable.Page;

        var total = query.Count();

        return new PaginatableResult<TEntity>()
        {
            Items = query.Skip(perPage * (page - 1)).Take(perPage).ToList(),
            Page = page,
            PerPage = perPage,
            Total = total,
            PageCount = (total + perPage - 1) / perPage,
        };      
    }
}
using Relaks.Models;
using Relaks.Models.Misc;
using Relaks.Models.Requests;
using Relaks.Utils.Extensions;

namespace Relaks.Database.Repositories;

public static class EntryRepository
{
    // public static IQueryable<BaseEntry> Search(this IQueryable<BaseEntry> q, string str)
    // {
    //     return q.Where(x => x.Name.Contains(str));
    // }

    public static PaginatableResult<BaseEntry> FilterByReq(this IQueryable<BaseEntry> q, EntryFilterRequest req)
    {
        if (!string.IsNullOrEmpty(req.Discriminator))
            q = q.Where(x => x.Discriminator.Equals(req.Discriminator));
        
        q = string.IsNullOrEmpty(req.OrderBy) ? q.OrderByDescending(x => x.UpdatedAt) : q.OrderBy(req);

        return q.Paginate(req);
    }
}
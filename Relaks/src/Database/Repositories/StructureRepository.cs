using Microsoft.EntityFrameworkCore;
using Relaks.Models.StructureModels;

namespace Relaks.Database.Repositories;

public class StructureGroupListRequest
{
    public Guid EntryId { get; set; }
    public DateTime Date { get; set; } = DateTime.UtcNow;
}

public static class StructureRepository
{
    public static List<StructureGroup> ToTree(this IQueryable<StructureGroup> query, StructureGroupListRequest req)
    {
        query = query
            .Include(x => x.Items).ThenInclude(x => x.Entry)
            .Where(x => x.EntryId.Equals(req.EntryId));
        
        return query.ToBaseTree();
    }
}
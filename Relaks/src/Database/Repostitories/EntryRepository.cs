using Relaks.Models;
using Relaks.Pages.Entries;

namespace Relaks.Database.Repostitories;

public static class EntryRepository
{
    public static IQueryable<BaseEntry> Search(this IQueryable<BaseEntry> q, string str)
    {
        return q.Where(x => x.Name.Contains(str));
    }
}
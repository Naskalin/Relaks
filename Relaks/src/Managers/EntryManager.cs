using Microsoft.EntityFrameworkCore;
using Relaks.Database;
using Relaks.Models;

namespace Relaks.Managers;

public class EntryManager
{
    private readonly AppDbContext _db;

    public EntryManager(AppDbContext db)
    {
        _db = db;
    }

    public void Delete(Guid entryId)
    {
        var entry = _db.BaseEntries
            .Include(x => x.EntryInfos)
            .FirstOrDefault(x => x.Id.Equals(entryId));

        if (entry?.DeletedAt == null) return;
        
        _db.BaseEntryInfos.RemoveRange(entry.EntryInfos);
        _db.BaseEntries.Remove(entry);
        _db.SaveChanges();
    }
}
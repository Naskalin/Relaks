using Relaks.Models;
using Microsoft.EntityFrameworkCore;
using Relaks.Database;
using Relaks.Database.Configurations;
using Relaks.Mappers;

namespace Relaks.Database.Events;

public static class EntryEvents
{
    // private static string FtsData(this BaseEntry baseEntry)
    // {
    //     var arr = new List<string?>
    //     {
    //         baseEntry.Name,
    //         baseEntry.Description,
    //         baseEntry.DeletedReason
    //     };
    //
    //     return String.Join(" ", arr.Where(x => !string.IsNullOrEmpty(x)));
    // }

    // public static void CheckAndRefresh(AppDbContext db)
    // {
    //     if (db.Entries.Count().Equals(db.Set<FtsEntry>().Count())) return;
    //
    //     db.Database.ExecuteSqlRaw("DELETE FROM Entries;");
    //     var rows = db.Entries.ToList();
    //     foreach (var row in rows)
    //     {
    //         Create(db, row);
    //     }
    // }
    
    public static void Create(AppDbContext db, BaseEntry baseEntry)
    {
        bool isValid = Guid.TryParse(baseEntry.Id.ToString(), out _);
        if (!isValid) return;
        
        db.Database.ExecuteSqlInterpolated(
            $"INSERT INTO FtsEntries(Id, Body) VALUES ({baseEntry.Id.ToString()}, {baseEntry.ToFtsBody()})"
        );
    }

    public static void Update(AppDbContext db, BaseEntry baseEntry)
    {
        bool isValid = Guid.TryParse(baseEntry.Id.ToString(), out _);
        if (!isValid) return;
        db.Database.ExecuteSqlInterpolated(
            $"UPDATE FtsEntries SET Body = {baseEntry.ToFtsBody()} WHERE Id = {baseEntry.Id.ToString()}"
        );
    }

    public static void Delete(AppDbContext db, BaseEntry baseEntry)
    {
        bool isValid = Guid.TryParse(baseEntry.Id.ToString(), out _);
        if (!isValid) return;
        db.Database.ExecuteSqlInterpolated(
            $"DELETE FROM FtsEntries WHERE Id = {baseEntry.Id.ToString()}"
        );
    }
}
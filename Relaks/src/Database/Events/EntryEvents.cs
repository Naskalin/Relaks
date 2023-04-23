using Relaks.Models;
using Microsoft.EntityFrameworkCore;
using Relaks.Database;
using Relaks.Mappers;

namespace Relaks.Database.Events;

public static class EntryEvents
{
    // private static string FtsData(this Entry entry)
    // {
    //     var arr = new List<string?>
    //     {
    //         entry.Name,
    //         entry.Description,
    //         entry.DeletedReason
    //     };
    //
    //     return String.Join(" ", arr.Where(x => !string.IsNullOrEmpty(x)));
    // }

    public static void CheckAndRefresh(AppDbContext db)
    {
        if (db.Entries.Count() == db.Set<FtsEntry>().Count()) return;

        db.Database.ExecuteSqlRaw("DELETE FROM FtsEntries;");
        var rows = db.Entries.ToList();
        foreach (var row in rows)
        {
            Create(db, row);
        }
    }
    
    public static void Create(AppDbContext db, Entry entry)
    {
        bool isValid = Guid.TryParse(entry.Id.ToString(), out _);
        if (!isValid) return;
        db.Database.ExecuteSqlInterpolated(
            $"INSERT INTO FtsEntries(Id, Body) VALUES ({entry.Id}, {entry.ToFtsBody()})"
        );
    }

    public static void Update(AppDbContext db, Entry entry)
    {
        bool isValid = Guid.TryParse(entry.Id.ToString(), out _);
        if (!isValid) return;
        db.Database.ExecuteSqlInterpolated(
            $"UPDATE FtsEntries SET Body = {entry.ToFtsBody()} WHERE Id = {entry.Id}"
        );
    }

    public static void Delete(AppDbContext db, Entry entry)
    {
        bool isValid = Guid.TryParse(entry.Id.ToString(), out _);
        if (!isValid) return;
        db.Database.ExecuteSqlInterpolated(
            $"DELETE FROM FtsEntries WHERE Id = {entry.Id}"
        );
    }
}
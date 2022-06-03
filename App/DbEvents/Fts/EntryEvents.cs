using App.DbConfigurations;
using App.Models;
using Microsoft.EntityFrameworkCore;

namespace App.DbEvents.Fts;

public static class EntryEvents
{
    private static string FtsData(Entry entry)
    {
        var arr = new List<string> {entry.Name};
        if (entry.Description != "") arr.Add(entry.Description);
        if (entry.DeletedReason != "") arr.Add(entry.DeletedReason);

        return String.Join(" ", arr);
    }

    public static void Create(AppDbContext db, Entry entry)
    {
        bool isValid = Guid.TryParse(entry.Id.ToString(), out _);
        if (!isValid) return;
        db.Database.ExecuteSqlInterpolated(
            $"INSERT INTO FtsEntries(Id, Data) VALUES ({entry.Id}, {FtsData(entry)})"
        );
    }

    public static void Update(AppDbContext db, Entry entry)
    {
        bool isValid = Guid.TryParse(entry.Id.ToString(), out _);
        if (!isValid) return;
        db.Database.ExecuteSqlInterpolated(
            $"UPDATE FtsEntries SET Data = {FtsData(entry)} WHERE Id = {entry.Id}"
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
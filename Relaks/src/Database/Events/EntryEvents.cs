using Relaks.Models;
using Microsoft.EntityFrameworkCore;
using Relaks.Mappers;

namespace Relaks.Database.Events;

public static class EntryEvents
{
    public static void Create(AppDbContext db, BaseEntry baseEntry)
    {
        bool isValid = Guid.TryParse(baseEntry.Id.ToString(), out _);
        if (!isValid) return;

        db.Database.ExecuteSqlInterpolated(
            $"INSERT INTO FtsEntries(Id, Body, Discriminator, DeletedAt) VALUES ({baseEntry.Id.ToString()}, {baseEntry.ToFtsBody()}, {baseEntry.Discriminator}, {baseEntry.DeletedAt.ToString()})"
        );

        // var ftsEntry = new FtsEntry()
        // {
        //     Id = baseEntry.Id,
        //     Body = baseEntry.ToFtsBody(),
        //     Discriminator = baseEntry.Discriminator,
        //     DeletedAt = baseEntry.DeletedAt?.ToString() ?? ""
        // };
        // db.Set<FtsEntry>().Add(ftsEntry);
        // db.SaveChanges();
    }

    public static void Update(AppDbContext db, BaseEntry baseEntry)
    {
        bool isValid = Guid.TryParse(baseEntry.Id.ToString(), out _);
        if (!isValid) return;
        db.Database.ExecuteSqlInterpolated(
            $"UPDATE FtsEntries SET Body = {baseEntry.ToFtsBody()} WHERE Id = {baseEntry.Id.ToString()}"
        );
        db.Database.ExecuteSqlInterpolated(
            $"UPDATE FtsEntries SET DeletedAt = {baseEntry.DeletedAt.ToString()} WHERE Id = {baseEntry.Id.ToString()}"
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
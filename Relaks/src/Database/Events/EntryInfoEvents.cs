using Relaks.Models;
using Microsoft.EntityFrameworkCore;
using Relaks.Mappers;

namespace Relaks.Database.Events;

public static class EntryInfoEvents
{

    public static void Create(AppDbContext db, BaseEntryInfo eInfo)
    {
        bool isValid = Guid.TryParse(eInfo.Id.ToString(), out _);
        if (!isValid) return;

        db.Database.ExecuteSqlInterpolated(
            $"INSERT INTO FtsEntryInfos(Id, EntryId, Body, Discriminator, DeletedAt) VALUES ({eInfo.Id.ToString()}, {eInfo.EntryId.ToString()}, {eInfo.ToFtsBody()}, {eInfo.Discriminator}, {eInfo.DeletedAt.ToString()})"
        );
    }

    public static void Update(AppDbContext db, BaseEntryInfo eInfo)
    {
        bool isValid = Guid.TryParse(eInfo.Id.ToString(), out _);
        if (!isValid) return;

        db.Database.ExecuteSqlInterpolated(
            $"UPDATE FtsEntryInfos SET Body = {eInfo.ToFtsBody()} WHERE Id = {eInfo.Id.ToString()}"
        );
        db.Database.ExecuteSqlInterpolated(
            $"UPDATE FtsEntryInfos SET DeletedAt = {eInfo.DeletedAt.ToString()} WHERE Id = {eInfo.Id.ToString()}"
        );
    }

    public static void Delete(AppDbContext db, BaseEntryInfo eInfo)
    {
        bool isValid = Guid.TryParse(eInfo.Id.ToString(), out _);
        if (!isValid) return;

        db.Database.ExecuteSqlInterpolated(
            $"DELETE FROM FtsEntryInfos WHERE Id = {eInfo.Id.ToString()}"
        );
    }
}
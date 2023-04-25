using Relaks.Models;
using Microsoft.EntityFrameworkCore;
using Relaks.Database;
using Relaks.Database.Configurations;
using Relaks.Mappers;

namespace Relaks.Database.Events;

public static class EntryInfoEvents
{
    // private static string FtsData(BaseEntryInfo eInfo)
    // {
    //     var arr = new List<string>();
    //     if (eInfo.Title != "") arr.Add(eInfo.Title);
    //     if (eInfo.DeletedReason != "") arr.Add(eInfo.DeletedReason);
    //
    //     switch (eInfo.Type.ToUpper())
    //     {
    //         case BaseEntryInfo.Email:
    //             arr.Add(eInfo.Email()!.Email);
    //             break;
    //         case BaseEntryInfo.Phone:
    //             arr.Add(eInfo.Phone()!.Number);
    //             break;
    //         case BaseEntryInfo.Url:
    //             arr.Add(eInfo.Url()!.Url);
    //             break;
    //         case BaseEntryInfo.Note:
    //             arr.Add(eInfo.Note()!.Note);
    //             break;
    //         case BaseEntryInfo.Custom:
    //             foreach (var group in eInfo.Custom()!.Groups)
    //             {
    //                 if (group.Title != "") arr.Add(group.Title);
    //
    //                 foreach (var item in group.Items)
    //                 {
    //                     if (item.Key != "") arr.Add(item.Key);
    //                     arr.Add(item.Value);
    //                 }
    //             }
    //             break;
    //     }
    //
    //     return String.Join(" ", arr);
    // }

    // public static void CheckAndRefresh(AppDbContext db)
    // {
    //     if (db.EntryInfos.Count().Equals(db.Set<FtsEntryInfo>().Count())) return;
    //
    //     db.Database.ExecuteSqlRaw("DELETE FROM FtsEntryInfos;");
    //     var rows = db.EntryInfos.ToList();
    //     foreach (var row in rows)
    //     {
    //         Create(db, row);
    //     }
    // }

    public static void Create(AppDbContext db, BaseEntryInfo eInfo)
    {
        bool isValid = Guid.TryParse(eInfo.Id.ToString(), out _);
        if (!isValid) return;

        db.Database.ExecuteSqlInterpolated(
            $"INSERT INTO FtsEntryInfos(Id, EntryId, Body) VALUES ({eInfo.Id.ToString()}, {eInfo.EntryId.ToString()}, {eInfo.ToFtsBody()})"
        );
    }

    public static void Update(AppDbContext db, BaseEntryInfo eInfo)
    {
        bool isValid = Guid.TryParse(eInfo.Id.ToString(), out _);
        if (!isValid) return;

        db.Database.ExecuteSqlInterpolated(
            $"UPDATE FtsEntryInfos SET Body = {eInfo.ToFtsBody()} WHERE Id = {eInfo.Id.ToString()}"
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
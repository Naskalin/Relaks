using App.DbConfigurations;
using App.Models;
using App.Utils;
using Microsoft.EntityFrameworkCore;

namespace App.DbEvents.Fts;

public static class EntryInfoEvents
{
    private static string FtsData(EntryInfo eInfo)
    {
        var arr = new List<string>();
        if (eInfo.Title != "") arr.Add(eInfo.Title);
        if (eInfo.DeletedReason != "") arr.Add(eInfo.DeletedReason);

        switch (eInfo.Type.ToUpper())
        {
            case EntryInfo.Email:
                arr.Add(eInfo.Email()!.Email);
                break;
            case EntryInfo.Phone:
                arr.Add(eInfo.Phone()!.Number);
                break;
            case EntryInfo.Url:
                arr.Add(eInfo.Url()!.Url);
                break;
            case EntryInfo.Note:
                arr.Add(eInfo.Note()!.Note);
                break;
            case EntryInfo.Custom:
                foreach (var group in eInfo.Custom()!.Groups)
                {
                    if (group.Title != "") arr.Add(group.Title);

                    foreach (var item in group.Items)
                    {
                        if (item.Key != "") arr.Add(item.Key);
                        arr.Add(item.Value);
                    }
                }
                break;
        }

        return String.Join(" ", arr);
    }
    
    public static void CheckAndRefresh(AppDbContext db)
    {
        var count = db.EntryInfos.Count();
        var ftsCount = db.Set<FtsEntryInfo>().Count();
        if (count == ftsCount) return;

        db.Database.ExecuteSqlRaw("DELETE FROM FtsEntryInfos;");
        var rows = db.EntryInfos.ToList();
        foreach (var row in rows)
        {
            Create(db, row);
        }
    }

    public static void Create(AppDbContext db, EntryInfo eInfo)
    {
        bool isValid = Guid.TryParse(eInfo.Id.ToString(), out _);
        if (!isValid) return;
        
        db.Database.ExecuteSqlInterpolated(
            $"INSERT INTO FtsEntryInfos(Id, EntryId, Data) VALUES ({eInfo.Id}, {eInfo.EntryId}, {FtsData(eInfo)})"
        );
    }
    
    public static void Update(AppDbContext db, EntryInfo eInfo)
    {
        bool isValid = Guid.TryParse(eInfo.Id.ToString(), out _);
        if (!isValid) return;
        
        db.Database.ExecuteSqlInterpolated(
            $"UPDATE FtsEntryInfos SET Data = {FtsData(eInfo)} WHERE Id = {eInfo.Id}"
        );
    }
    
    public static void Delete(AppDbContext db, EntryInfo eInfo)
    {
        bool isValid = Guid.TryParse(eInfo.Id.ToString(), out _);
        if (!isValid) return;
        
        db.Database.ExecuteSqlInterpolated(
            $"DELETE FROM FtsEntryInfos WHERE Id = {eInfo.Id}"
        );
    }
}
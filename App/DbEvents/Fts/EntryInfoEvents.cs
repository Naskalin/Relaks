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

        if (InfoBaseType.Email.Equals(eInfo.Type))
            arr.Add(eInfo.Email()!.Email);
        else if (InfoBaseType.Phone.Equals(eInfo.Type))
            arr.Add(eInfo.Phone()!.Number);
        else if (InfoBaseType.Url.Equals(eInfo.Type))
            arr.Add(eInfo.Url()!.Url);
        else if (InfoBaseType.Note.Equals(eInfo.Type))
            arr.Add(eInfo.Note()!.Note);

        return String.Join(" ", arr);
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
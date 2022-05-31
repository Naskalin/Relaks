using App.DbConfigurations;
using App.Models;
using App.Utils;
using Microsoft.EntityFrameworkCore;

namespace App.DbEvents;

public static class EntryInfoEvents
{
    private static string FtsData(EntryInfo eInfo)
    {
        var arr = new List<string>();
        if (eInfo.Title != "") arr.Add(eInfo.Title);
        if (eInfo.DeletedReason != "") arr.Add(eInfo.DeletedReason);
        
        switch (eInfo.Type)
        {
            case EntryInfoType.Email:
                arr.Add(eInfo.Email()!.Email);
                break;
            case EntryInfoType.Phone:
                arr.Add(eInfo.Phone()!.Number);
                break;
            case EntryInfoType.Url:
                arr.Add(eInfo.Url()!.Url);
                break;
            case EntryInfoType.Note:
                arr.Add(eInfo.Note()!.Note);
                break;
        }

        return String.Join(" ", arr);
    }
    
    public static void Create(AppDbContext db, EntryInfo eInfo)
    {
        db.Database.ExecuteSqlRaw(
            FtsHelper.InsertRaw(new InsertModel()
            {
                TableName = "FtsEntryInfos",
                Data = new Dictionary<string, object>()
                {
                    {"Id", eInfo.Id},
                    {"EntryId", eInfo.EntryId},
                    {"Data", FtsData(eInfo)}
                }
            })
        );
    }
    
    public static void Update(AppDbContext db, EntryInfo eInfo)
    {
        db.Database.ExecuteSqlRaw(
            FtsHelper.UpdateRaw(new UpdateModel()
            {
                TableName = "FtsEntryInfos",
                Data = new Dictionary<string, object>()
                {
                    {"Data", FtsData(eInfo)}
                },
                WhereId = eInfo.Id
            })
        );
    }
    
    public static void Delete(AppDbContext db, EntryInfo eInfo)
    {
        db.Database.ExecuteSqlRaw(
            FtsHelper.DeleteRaw(new DeleteModel()
            {
                TableName = "FtsEntryInfos",
                WhereId = eInfo.Id
            })
        );
    }
}
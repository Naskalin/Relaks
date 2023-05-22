using Microsoft.EntityFrameworkCore;
using Relaks.Database.DbUtils;
using Relaks.Mappers;
using Relaks.Models;

namespace Relaks.Database.Events;

public class FileEvents
{
    public static void Create(AppDbContext db, BaseFile baseFile)
    {
        bool isValid = Guid.TryParse(baseFile.Id.ToString(), out _);
        if (!isValid) return;

        db.Database.ExecuteSqlInterpolated($"INSERT INTO FtsFiles(Id, Body, Discriminator, DeletedAt) VALUES ({baseFile.Id.ToString()}, {baseFile.ToFtsBody()}, {baseFile.Discriminator}, {baseFile.DeletedAt.ToString()})");
        // var str = FtsMigrationHelper.InsertHelper(new Dictionary<string, string>()
        // {
        //     {nameof(FtsFile.Id), baseFile.Id.ToString()},
        //     {nameof(FtsFile.Body), baseFile.ToFtsBody()},
        //     {nameof(FtsFile.Discriminator), baseFile.Discriminator},
        //     {nameof(FtsFile.DeletedAt), baseFile.DeletedAt?.ToString() ?? ""},
        // });
        // System.Console.WriteLine(str);
        // System.Console.WriteLine($"INSERT INTO FtsFiles(Id, Body, Discriminator, DeletedAt) VALUES ({baseFile.Id.ToString()}, {baseFile.ToFtsBody()}, {baseFile.Discriminator}, {baseFile.DeletedAt.ToString()})");
        // var str = FtsMigrationHelper.InsertHelper(new Dictionary<string, string>()
        // {
        //     {nameof(FtsFile.Id), baseFile.Id.ToString()},
        //     {nameof(FtsFile.Body), baseFile.ToFtsBody()},
        //     {nameof(FtsFile.Discriminator), baseFile.Discriminator},
        //     {nameof(FtsFile.DeletedAt), baseFile.DeletedAt.ToString() ?? ""},
        // });
        // db.Database.ExecuteSqlInterpolated(
        //     $"{str}"
        // );
    }

    public static void Update(AppDbContext db, BaseFile baseFile)
    {
        bool isValid = Guid.TryParse(baseFile.Id.ToString(), out _);
        if (!isValid) return;
        db.Database.ExecuteSqlInterpolated(
            $"UPDATE FtsFiles SET Body = {baseFile.ToFtsBody()} WHERE Id = {baseFile.Id.ToString()}"
        );
        db.Database.ExecuteSqlInterpolated(
            $"UPDATE FtsFiles SET DeletedAt = {baseFile.DeletedAt.ToString()} WHERE Id = {baseFile.Id.ToString()}"
        );
    }

    public static void Delete(AppDbContext db, BaseFile baseFile)
    {
        bool isValid = Guid.TryParse(baseFile.Id.ToString(), out _);
        if (!isValid) return;
        db.Database.ExecuteSqlInterpolated(
            $"DELETE FROM FtsFiles WHERE Id = {baseFile.Id.ToString()}"
        );
    }
}
using Bogus;
using Microsoft.EntityFrameworkCore;
using Relaks.Database;

namespace Relaks.Database.Seeders;

public partial class DatabaseSeeder
{
    protected readonly AppDbContext Db;
    protected readonly Faker Faker;

    public DatabaseSeeder(AppDbContext db)
    {
        Db = db;
        Faker = new Faker("ru");
    }

    public void SeedAll()
    {
        // clear fts
        Db.Database.ExecuteSqlRaw("DELETE FROM FtsEntryInfos;");
        Db.Database.ExecuteSqlRaw("DELETE FROM Entries;");
        
        Db.Database.ExecuteSqlRaw("DELETE FROM EntryInfos;");
        Db.Database.ExecuteSqlRaw("DELETE FROM Entries;");
        // Db.Database.ExecuteSqlRaw("delete from StructureItems;");
        // Db.Database.ExecuteSqlRaw("delete from StructureConnections;");
        // Db.Database.ExecuteSqlRaw("delete from Structures;");
        // Db.Database.ExecuteSqlRaw("delete from InfoTemplates;");

        SeedEntries();
        SeedEntryInfos();
        // SeedStructures();
        // SeedInfoTemplate();
    }
}
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
        Db.Database.ExecuteSqlRaw("delete from EntryInfos;");
        // Db.Database.ExecuteSqlRaw("delete from FtsEntryInfos;");
        Db.Database.ExecuteSqlRaw("delete from Entries;");
        Db.Database.ExecuteSqlRaw("delete from FtsEntries;");
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
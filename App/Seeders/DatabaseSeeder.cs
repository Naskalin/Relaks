using App.DbConfigurations;
using Bogus;
using Microsoft.EntityFrameworkCore;

namespace App.Seeders;

public class DatabaseSeeder
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
        Db.Database.ExecuteSqlRaw("delete from FtsEntryInfos;");
        Db.Database.ExecuteSqlRaw("delete from Entries;");
        Db.Database.ExecuteSqlRaw("delete from FtsEntries;");

        new EntrySeeder(Db).Seed();
        new EntryInfoSeeder(Db).Seed();
    }
}
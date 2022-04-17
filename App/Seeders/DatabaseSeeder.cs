using App.DbConfigurations;
using App.Models;
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
        Db.Database.ExecuteSqlRaw("delete from Entries;");

        new EntrySeeder(Db).Seed();
        new EntryDateSeeder(Db).Seed();
        new EntryNoteSeeder(Db).Seed();
        new EntryPhoneSeeder(Db).Seed();
        new EntryUrlSeeder(Db).Seed();
        new EntryEmailSeeder(Db).Seed();
    }
}
using App.DbConfigurations;
using App.Models;
using Bogus;

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

    public async Task SeedAll()
    {
        await new EntrySeeder(Db).Seed();
        await new EntryInfoDateSeeder(Db).Seed();
        // await new EntryTextSeeder(Db).Seed();
    }
}
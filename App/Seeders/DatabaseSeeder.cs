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
        Db.Entries.RemoveRange(Db.Entries);
        // Db.Set<EntryFts>().RemoveRange(Db.Set<EntryFts>());
        Db.Set<EntryInfo>().RemoveRange(Db.Set<EntryInfo>());

        // await new EntrySeeder(Db).Seed();
        await new EntryDateSeeder(Db).Seed();
        await new EntryNoteSeeder(Db).Seed();
        await new EntryPhoneSeeder(Db).Seed();
        await new EntryUrlSeeder(Db).Seed();
        await new EntryEmailSeeder(Db).Seed();
    }
}
using App.DbConfigurations;
using App.Models;

namespace App.Seeders;

public class DatabaseSeeder
{
    protected readonly AppDbContext Db;

    public DatabaseSeeder(AppDbContext db)
    {
        Db = db;
    }

    public async Task SeedAll()
    {
        await new EntrySeeder(Db).Seed();
        await new EntryDateSeeder(Db).Seed();
        await new EntryTextSeeder(Db).Seed();
    }
}
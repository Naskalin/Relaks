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
        await new InfoDateSeeder(Db).Seed();
        await new InfoTextSeeder(Db).Seed();
    }
}
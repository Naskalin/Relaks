using App.Data;

namespace App.Seeders;

public class DatabaseSeeder
{
    protected readonly AppDbContext Db;

    public DatabaseSeeder(AppDbContext db)
    {
        Db = db;
    }

    public async Task seed()
    {
        new EntrySeeder(Db).Seed();
        
        await Db.SaveChangesAsync();   
    }
}
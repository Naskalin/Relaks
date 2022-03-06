using App.Data;

namespace App.Seeders;

public class DatabaseSeeder
{
    protected readonly AppDbContext Db;

    protected DatabaseSeeder(AppDbContext db)
    {
        Db = db;
    }
}
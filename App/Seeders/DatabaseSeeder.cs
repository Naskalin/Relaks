namespace App.Seeders;

public class DatabaseSeeder
{
    protected readonly ApplicationContext Db;

    protected DatabaseSeeder(ApplicationContext db)
    {
        Db = db;
    }
}
using App.DbConfigurations;

namespace App.Repository;

public abstract class BaseRepository
{
    protected readonly AppDbContext Db;

    protected BaseRepository(AppDbContext db)
    {
        Db = db;
    }

    public async Task SaveChangesAsync()
    {
        await Db.SaveChangesAsync();
    }
}
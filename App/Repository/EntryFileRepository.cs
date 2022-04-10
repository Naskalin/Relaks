using App.DbConfigurations;
using App.Models;

namespace App.Repository;

public class EntryFileRepository : BaseRepository<EntryFile>
{
    public EntryFileRepository(AppDbContext db) : base(db)
    {
    }
}
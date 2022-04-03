using App.DbConfigurations;
using App.Models;

namespace App.Repository;

public class EntryUrlRepository : BaseRepository<EntryUrl>
{
    public EntryUrlRepository(AppDbContext db) : base(db)
    {
    }
}
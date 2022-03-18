using App.DbConfigurations;
using App.Models;

namespace App.Repository;

public class EntryTextRepository : BaseRepository<EntryText>
{
    public EntryTextRepository(AppDbContext db) : base(db)
    {
    }
}
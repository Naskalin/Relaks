using App.DbConfigurations;
using App.Models;

namespace App.Repository;

public class EntryNoteRepository : BaseRepository<EntryNote>
{
    public EntryNoteRepository(AppDbContext db) : base(db)
    {
    }
}
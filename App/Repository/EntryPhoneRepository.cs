using App.DbConfigurations;
using App.Models;

namespace App.Repository;

public class EntryPhoneRepository : BaseRepository<EntryPhone>
{
    public EntryPhoneRepository(AppDbContext db) : base(db)
    {
    }
}
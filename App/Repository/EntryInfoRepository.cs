using App.DbConfigurations;
using App.Models;

namespace App.Repository;

public class EntryInfoRepository : BaseRepository<EntryInfo>
{
    public EntryInfoRepository(AppDbContext db) : base(db)
    {
    }
}
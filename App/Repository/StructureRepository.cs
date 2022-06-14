using App.DbConfigurations;
using App.Models;

namespace App.Repository;

public class StructureRepository : BaseRepository<Structure>
{
    public StructureRepository(AppDbContext db) : base(db)
    {
    }
}
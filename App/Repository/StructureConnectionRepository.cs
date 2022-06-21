using App.DbConfigurations;
using App.Endpoints.StructureConnections;
using App.Models;
using Microsoft.EntityFrameworkCore;

namespace App.Repository;

public class StructureConnectionRepository : BaseRepository<StructureConnection>
{
    public StructureConnectionRepository(AppDbContext db) : base(db)
    {
    }

    public IQueryable<StructureConnection> FindByEntryId(Guid entryId)
    {
        return Entities.Where(x =>
            x.StructureFirst.EntryId.Equals(entryId) || x.StructureSecond.EntryId.Equals(entryId)
        );
    }
}
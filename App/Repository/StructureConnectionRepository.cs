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

    public IQueryable<StructureConnection> FindByListRequest(StructureConnectionListRequest req)
    {
        var query = FindByEntryId(req.EntryId);
        
        if (req.IsDeleted != null)
        {
            if (req.IsDeleted == true)
            {
                query = query.Where(x => x.DeletedAt != null && x.DeletedAt > DateTime.UtcNow);
            }
            else
            {
                // false
                query = query.Where(x => x.DeletedAt == null || x.DeletedAt < DateTime.UtcNow);
            }
        }
        
        if (req.Date != null)
        {
            query = query.Where(x => x.StartAt <= req.Date);
        }

        return query;
    }
}
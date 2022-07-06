using App.DbConfigurations;
using App.Endpoints.Structures;
using App.Models;
using App.Utils.Extensions.Database;
using Microsoft.EntityFrameworkCore;

namespace App.Repository;

public class StructureRepository : BaseRepository<Structure>
{
    public StructureRepository(AppDbContext db) : base(db)
    {
    }

    public IQueryable<Structure> FindStructures(ListRequest req)
    {
        var query = Entities
            .Where(x => x.EntryId.Equals(req.EntryId));

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

        query = query.OrderBy(x => x.Title);

        if (req.Date != null)
        {
            query = query.Where(x => x.StartAt <= req.Date);
        }

        return query.OrderBy(x => x.Title);
    }
}
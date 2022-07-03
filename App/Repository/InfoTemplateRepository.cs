using App.DbConfigurations;
using App.Endpoints.InfoTemplates;
using App.Models;

namespace App.Repository;

public class InfoTemplateRepository : BaseRepository<InfoTemplate>
{
    public InfoTemplateRepository(AppDbContext db) : base(db)
    {
    }

    public IQueryable<InfoTemplate> FindByListRequest(ListRequest req)
    {
        var query = Entities.AsQueryable();
        
        query = query.OrderByDescending(x => x.UpdatedAt);
        if (req.Page != null && req.PerPage != null)
        {
            query = PaginateQuery(query, req);
        }

        return query;
    }
}
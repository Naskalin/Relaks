using App.DbConfigurations;
using App.Endpoints.FileCategories;
using App.Models;

namespace App.Repository;

public class FileCategoryRepository : BaseRepository<FileCategory>
{
    public FileCategoryRepository(AppDbContext db) : base(db)
    {
    }

    public IQueryable<FileCategory> FindForListRequest(FileCategoryListRequest req)
    {
        var query = Entities.AsQueryable();
    
        if (req.IsDeleted != null)
            query = query.Where(x => req.IsDeleted == true ? x.DeletedAt != null : x.DeletedAt == null);

        return query;
    }
}
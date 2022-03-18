using App.DbConfigurations;
using App.Endpoints.Entries;
using App.Models;
using Microsoft.EntityFrameworkCore;

namespace App.Repository;

public class BaseRepository<TEntity> where TEntity : BaseEntity
{
    protected readonly AppDbContext Db;
    protected DbSet<TEntity> Entities;

    public BaseRepository(AppDbContext db)
    {
        Db = db;
        Entities = db.Set<TEntity>();
    }

    public async Task<TEntity?> FindByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await Entities.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    protected IQueryable<TEntity> PaginateQuery(int page, int perPage)
    {
        return Entities.Skip(perPage * (page - 1)).Take(perPage);
    }

    public virtual async Task<List<TEntity>> PaginateListAsync(int perPage, int page, CancellationToken cancellationToken)
    {
        return await PaginateQuery(perPage, page).ToListAsync(cancellationToken);
    }

    public async Task CreateAsync(TEntity entity, CancellationToken cancellationToken)
    {
        await Entities.AddAsync(entity, cancellationToken);
        await Db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken)
    {
        Db.Entry(entity).State = EntityState.Modified;
        await Db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken)
    {
        Entities.Remove(entity);
        await Db.SaveChangesAsync(cancellationToken);
    }
}
using App.DbConfigurations;
using App.Endpoints;
using App.Models;
using Microsoft.EntityFrameworkCore;

namespace App.Repository;

public class FtsResult 
{
    public Guid Id { get; set; }
    public string Snippet { get; set; } = null!;
}

public class BaseRepository<TEntity> where TEntity : BaseEntity
{
    public readonly AppDbContext Db;
    public DbSet<TEntity> Entities;

    public BaseRepository(AppDbContext db)
    {
        Db = db;
        Entities = db.Set<TEntity>();
    }

    public async Task<TEntity?> FindByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await Entities.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public IQueryable<TEntity> PaginateQuery(IQueryable<TEntity> query, IPaginableRequest request)
    {
        var perPage = request.PerPage ?? 50;
        var page = request.Page ?? 1;
        
        return query
            .Skip(perPage * (page - 1))
            .Take(perPage);
    }

    public async Task CreateAsync(TEntity entity, CancellationToken cancellationToken)
    {
        await Entities.AddAsync(entity, cancellationToken);
        await Db.SaveChangesAsync(cancellationToken);
    }

    public async Task CreateRangeAsync(List<TEntity> entities, CancellationToken cancellationToken)
    {
        Entities.AddRange(entities);
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
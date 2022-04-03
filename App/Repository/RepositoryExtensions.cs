using App.Models;

namespace App.Repository;

public static class RepositoryExtensions
{
    public static async Task TrySoftDelete<TEntity>(
        this BaseRepository<TEntity> baseRepository,
        TEntity entity,
        CancellationToken cancellationToken) where TEntity : BaseEntity, ISoftDelete
    {
        if (entity.DeletedAt == null)
        {
            entity.DeletedAt = DateTime.UtcNow;
        }
        else
        {
            baseRepository.Entities.Remove(entity);
        }

        await baseRepository.Db.SaveChangesAsync(cancellationToken);
    }
}
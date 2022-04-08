// using App.Models;
//
// namespace App.Repository;
//
// public static class RepositoryExtensions
// {
//     public static async Task SoftDeleteAsync<TEntity>(
//         this BaseRepository<TEntity> baseRepository,
//         TEntity entity,
//         CancellationToken cancellationToken) where TEntity : BaseEntity, ISoftDelete
//     {
//         entity.DeletedAt = DateTime.UtcNow;
//         await baseRepository.UpdateAsync(entity, cancellationToken);
//     }
// }
using Relaks.Interfaces;

namespace Relaks.Mappers;

public static class MiscMapper
{
    public static void MapSoftDeleted(this ISoftDeletedReason from, ISoftDeletedReason to)
    {
        to.DeletedAt = from.DeletedAt;
        to.DeletedReason = from.DeletedReason?.Trim();

        if (string.IsNullOrEmpty(to.DeletedReason)) to.DeletedReason = null;
        if (to.DeletedAt == null && to.DeletedReason != null)
        {
            to.DeletedAt = DateTime.UtcNow;
        }
    }
}
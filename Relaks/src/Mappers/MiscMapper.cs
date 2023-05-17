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
    
    public static void MapPhone(this IPhone from, IPhone to)
    {
        to.Region = from.Region.ToUpper();
        to.Number = from.Number;
    }
    
    public static void MapEmail(this IEmail from, IEmail to)
    {
        to.Email = from.Email.Trim();
    }
    
    public static void MapUrl(this IUrl from, IUrl to)
    {
        to.Url = from.Url.Trim();
    }
    
    public static void MapDate(this IDate from, IDate to)
    {
        to.Date = from.Date;
        to.WithTime = from.WithTime;
    }
}
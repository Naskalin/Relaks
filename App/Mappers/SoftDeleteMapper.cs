using App.Models;

namespace App.Mappers;

public static class SoftDeleteMapper
{
    public static void SoftDeleteMapTo(this ISoftDelete req, ISoftDelete model)
    {
        model.DeletedAt = req.DeletedAt;
        var reqReason = req.DeletedReason.Trim();
        model.DeletedReason = reqReason;
        if (req.DeletedAt == null && reqReason != "")
        {
            model.DeletedAt = DateTime.UtcNow;
        }
    }
}
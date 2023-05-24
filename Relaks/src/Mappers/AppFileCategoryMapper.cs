using Relaks.Models;

namespace Relaks.Mappers;

public static class AppFileCategoryMapper
{
    public static void MapTo(this BaseFileCategory from, BaseFileCategory to)
    {
        to.Title = from.Title.Trim();
        to.ParentId = from.ParentId;
    }
}
using Relaks.Models;

namespace Relaks.Mappers;

public static class AppFileTagMapper
{
    public static void MapTo(this BaseFileTag from, BaseFileTag to)
    {
        to.Title = from.Title.Trim();
    }
}
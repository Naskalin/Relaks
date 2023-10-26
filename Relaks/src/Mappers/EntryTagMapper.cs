using Relaks.Models;

namespace Relaks.Mappers;

public static class EntryTagMapper
{
    public static void MapTo(this EntryTagCategory from, EntryTagCategory to)
    {
        to.Id = from.Id;
        to.ParentId = from.ParentId;
        to.Title = from.Title;
    }
}
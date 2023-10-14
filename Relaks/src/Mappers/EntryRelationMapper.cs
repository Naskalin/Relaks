using Relaks.Interfaces;

namespace Relaks.Mappers;

public static class EntryRelationMapper
{
    public static void MapTo(this IEntryRelation from, IEntryRelation to)
    {
        to.FirstId = from.FirstId;
        to.SecondId = from.SecondId;
        to.FirstRating = from.FirstRating;
        to.SecondRating = from.SecondRating;
        to.Description = from.Description;
        // to.FirstDescription = from.FirstDescription;
        // to.SecondDescription = from.SecondDescription;
    }
}
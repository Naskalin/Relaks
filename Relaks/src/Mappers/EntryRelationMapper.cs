using Relaks.Interfaces;
using Relaks.Models.Store;

namespace Relaks.Mappers;

public static class EntryRelationMapper
{
    public static void MapTo(this EntryRelationRequest from, IEntryRelation to)
    {
        ArgumentNullException.ThrowIfNull(from.FirstId);
        ArgumentNullException.ThrowIfNull(from.SecondId);
        
        to.FirstId = from.FirstId.Value;
        to.SecondId = from.SecondId.Value;
        to.FirstRating = from.FirstRating;
        to.SecondRating = from.SecondRating;
        to.Description = from.Description;
    }
    
    public static void MapTo(this IEntryRelation from, EntryRelationRequest to)
    {
        to.FirstId = from.FirstId;
        to.SecondId = from.SecondId;
        to.FirstRating = from.FirstRating;
        to.SecondRating = from.SecondRating;
        to.Description = from.Description;
    }
}
using Relaks.Models.StructureModels;

namespace Relaks.Mappers;

public static class StructureMapper
{
    public static void MapTo(this StructureGroup group, StructureGroup model)
    {
        model.Title = group.Title;
        model.Description = group.Description;
        model.StartAt = group.StartAt;
        model.EndAt = group.EndAt;
        model.ParentId = group.ParentId;
    }
    
    public static void MapTo(this StructureItem item, StructureItem model)
    {
        model.Title = item.Title;
        model.Description = item.Description;
        model.StartAt = item.StartAt;
        model.EndAt = item.EndAt;
        model.EntryId = item.EntryId;
        model.GroupId = item.GroupId;
    }
}
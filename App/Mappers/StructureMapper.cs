using App.Endpoints.StructureItems;
using App.Endpoints.Structures;
using App.Models;

namespace App.Mappers;

public static class StructureMapper
{
    public static void MapTo(this CreateStructureRequestDetails details, Structure structure)
    {
        structure.Description = details.Description.Trim();
        structure.Title = details.Title.Trim();
        structure.StartAt = details.StartAt ?? DateTime.UtcNow;
        structure.ParentId = details.ParentId;
        structure.UpdatedAt = DateTime.UtcNow;
        
        details.SoftDeleteMapTo(structure);
    }

    public static void MapTo(this StructureItemFormDetails details, StructureItem structureItem)
    {
        structureItem.UpdatedAt = DateTime.UtcNow;
        structureItem.Comment = details.Comment.Trim();
        structureItem.StartAt = details.StartAt ?? DateTime.UtcNow;
        structureItem.EntryId = details.EntryId;
        structureItem.StructureId = details.StructureId;

        details.SoftDeleteMapTo(structureItem);
    }
}
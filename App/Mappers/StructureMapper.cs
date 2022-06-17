using App.Endpoints.Entries.EntryStructures;
using App.Models;

namespace App.Mappers;

public static class StructureMapper
{
    public static void MapTo(this CreateStructureRequestDetails details, Structure structure)
    {
        structure.Description = details.Description.Trim();
        structure.Title = details.Title.Trim();
        structure.StartAt = details.StartAt;
        structure.ParentId = details.ParentId;
        structure.UpdatedAt = DateTime.UtcNow;
        
        details.SoftDeleteMapTo(structure);
    }
}
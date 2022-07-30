using App.Endpoints.StructureConnections;
using App.Endpoints.StructureItems;
using App.Endpoints.Structures;
using App.Models;

namespace App.Mappers;

public static class StructureMapper
{
    public static void MapTo(this StructureFormRequest formReq, Structure structure)
    {
        structure.Description = formReq.Description.Trim();
        structure.Title = formReq.Title.Trim();
        structure.StartAt = formReq.StartAt ?? DateTime.UtcNow;
        structure.ParentId = formReq.ParentId;
        structure.UpdatedAt = DateTime.UtcNow;

        formReq.MapToSoftDelete(structure);
    }

    public static void MapTo(this StructureItemFormRequest formReq, StructureItem structureItem)
    {
        structureItem.UpdatedAt = DateTime.UtcNow;
        structureItem.Description = formReq.Description.Trim();
        structureItem.StartAt = formReq.StartAt ?? DateTime.UtcNow;
        structureItem.EntryId = formReq.EntryId;
        structureItem.StructureId = formReq.StructureId;

        formReq.MapToSoftDelete(structureItem);
    }

    public static void MapTo(this StructureConnectionFormRequest formReq, StructureConnection structureConnection)
    {
        structureConnection.UpdatedAt = DateTime.UtcNow;
        structureConnection.Description = formReq.Description.Trim();
        structureConnection.Direction = formReq.Direction;
        structureConnection.StartAt = formReq.StartAt ?? DateTime.UtcNow;
        structureConnection.StructureFirstId = formReq.StructureFirstId;
        structureConnection.StructureSecondId = formReq.StructureSecondId;
        
        formReq.MapToSoftDelete(structureConnection);
    }
}
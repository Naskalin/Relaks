// using App.Endpoints.StructureConnections;
// using App.Endpoints.StructureItems;
// using App.Endpoints.Structures;
// using App.Models;
//
// namespace App.Mappers;
//
// public static class StructureMapper
// {
//     public static void MapTo(this CreateStructureRequestDetails details, Structure structure)
//     {
//         structure.Description = details.Description.Trim();
//         structure.Title = details.Title.Trim();
//         structure.StartAt = details.StartAt ?? DateTime.UtcNow;
//         structure.ParentId = details.ParentId;
//         structure.UpdatedAt = DateTime.UtcNow;
//
//         details.SoftDeleteMapTo(structure);
//     }
//
//     public static void MapTo(this StructureItemFormDetails details, StructureItem structureItem)
//     {
//         structureItem.UpdatedAt = DateTime.UtcNow;
//         structureItem.Description = details.Description.Trim();
//         structureItem.StartAt = details.StartAt ?? DateTime.UtcNow;
//         structureItem.EntryId = details.EntryId;
//         structureItem.StructureId = details.StructureId;
//
//         details.SoftDeleteMapTo(structureItem);
//     }
//
//     public static void MapTo(this StructureConnectionFormDetails details, StructureConnection structureConnection)
//     {
//         structureConnection.UpdatedAt = DateTime.UtcNow;
//         structureConnection.Description = details.Description.Trim();
//         structureConnection.Direction = details.Direction;
//         structureConnection.StartAt = details.StartAt ?? DateTime.UtcNow;
//         structureConnection.StructureFirstId = details.StructureFirstId;
//         structureConnection.StructureSecondId = details.StructureSecondId;
//         
//         details.SoftDeleteMapTo(structureConnection);
//     }
// }
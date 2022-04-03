// using App.Endpoints.Entries.Dates;
// using App.Models;
//
// namespace App.Mappers;
//
// public static class EntryDateMapper
// {
//     private static void MapToCreateDetails(EntryDateDetails req, EntryDate entryDate)
//     {
//         entryDate.Title = req.Title.Trim();
//         entryDate.Val = req.Val;
//         
//         entryDate.DeletedAt = req.DeletedAt;
//         entryDate.DeletedReason = req.DeletedReason.Trim();
//     }
//     public static void MapTo(this CreateRequest req, EntryDate entryDate)
//     {
//         MapToCreateDetails(req.Details, entryDate);
//     }
//
//     public static void MapTo(this PutRequest req, EntryDate entryDate)
//     {
//         MapToCreateDetails(req.Details, entryDate);
//     }
// }
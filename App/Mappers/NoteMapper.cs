// using App.Models;
//
// namespace App.Mappers;
//
// public static class NoteMapper
// {
//     public static void MapTo(this NoteCreateDto dto, Note note)
//     {
//         note.Description = dto.Description;
//         note.EntryId = dto.EntryId;
//         note.Title = dto.Title;
//     }
//
//     public static void MapTo(this NotePatchDto dto, Note note)
//     {
//         if (!dto.Title.IsUndefined)
//         {
//             note.Title = dto.Title.Value;
//         }
//
//         if (!dto.EntryId.IsUndefined)
//         {
//             note.EntryId = dto.EntryId.Value;
//         }
//
//         if (!dto.Description.IsUndefined)
//         {
//             note.Description = dto.Description.Value;
//         }
//     }
// }
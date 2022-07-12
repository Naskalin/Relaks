// using System.Diagnostics;
// using App.Repository;
// using App.Utils;
// using Ardalis.ApiEndpoints;
// using Microsoft.AspNetCore.Mvc;
// using Swashbuckle.AspNetCore.Annotations;
//
// namespace App.Endpoints.Entries.EntryFiles;
//
// public class Delete : EndpointBaseAsync
//     .WithRequest<GetRequest>
//     .WithActionResult
// {
//     private readonly EntryFileRepository _entryFileRepository;
//     private readonly EntryRepository _entryRepository;
//     private readonly AppPreset _appPreset;
//
//     public Delete(EntryFileRepository entryFileRepository, EntryRepository entryRepository, AppPreset appPreset)
//     {
//         _entryFileRepository = entryFileRepository;
//         _entryRepository = entryRepository;
//         _appPreset = appPreset;
//     }
//
//     [HttpDelete("/api/entries/{entryId}/files/{entryFileId}")]
//     [SwaggerOperation(OperationId = "EntryFile.Delete", Tags = new[] {"EntryFile"})]
//     public override async Task<ActionResult> HandleAsync(
//         [FromMultiSource] GetRequest req,
//         CancellationToken cancellationToken = new()
//     )
//     {
//         var entryFile = await _entryFileRepository.FindByIdAsync(req.EntryFileId, cancellationToken);
//         if (entryFile == null || entryFile.EntryId != req.EntryId)
//         {
//             return NotFound();
//         }
//         
//         await _entryFileRepository.DeleteAsync(entryFile, cancellationToken);
//         
//         // Remove attached file
//         var filePath = Path.Combine(_appPreset.FilesDir, entryFile.GetFileRelativePath());
//         if (System.IO.File.Exists(filePath)) System.IO.File.Delete(filePath);
//         
//         // Remove if its avatar
//         var entry = await _entryRepository.FindByIdAsync(req.EntryId, cancellationToken);
//         if (entry!.Avatar == entryFile.Id)
//         {
//             entry.Avatar = null;
//             await _entryRepository.UpdateAsync(entry, cancellationToken);
//         }
//         
//         return NoContent();
//     }
// }
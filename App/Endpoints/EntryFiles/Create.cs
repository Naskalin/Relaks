﻿// using App.Mappers;
// using App.Models;
// using App.Repository;
// using App.Utils;
// using App.Utils.App;
// using Ardalis.ApiEndpoints;
// using Microsoft.AspNetCore.Mvc;
// using Swashbuckle.AspNetCore.Annotations;
//
// namespace App.Endpoints.Entries.EntryFiles;
//
// public class Create : EndpointBaseAsync
//     .WithRequest<CreateRequest>
//     .WithActionResult
//
// {
//     private readonly EntryFileRepository _entryFileRepository;
//     private readonly EntryRepository _entryRepository;
//     private readonly AppPresetModel? _appPreset;
//
//     public Create(EntryFileRepository entryFileRepository, EntryRepository entryRepository, IConfiguration configuration)
//     {
//         _entryFileRepository = entryFileRepository;
//         _entryRepository = entryRepository;
//         _appPreset = AppPresetManager.GetPreset(configuration.GetValue<string>(WebHostDefaults.ContentRootKey));
//     }
//
//     [HttpPost("/api/entries/{entryId}/files")]
//     [SwaggerOperation(OperationId = "EntryFile.Create", Tags = new[] {"EntryFile"})]
//     public override async Task<ActionResult> HandleAsync(
//         [FromMultiSource] CreateRequest request,
//         CancellationToken cancellationToken = new()
//     )
//     {
//         var entry = await _entryRepository.FindByIdAsync(request.EntryId, cancellationToken);
//         if (entry == null)
//         {
//             return NotFound();
//         }
//
//         var size = request.Files.Sum(f => f.Length);
//         var category = "";
//         if (request.Category != null) category = request.Category.Trim();
//         
//         foreach (var formFile in request.Files)
//         {
//             if (formFile.Length <= 0) continue;
//
//             var entryFile = new EntryFile
//             {
//                 EntryId = entry.Id,
//                 Category = category
//             };
//             formFile.MapToCreate(entryFile);
//
//             var folderFull = Path.Combine(_appPreset!.FilesDir, entryFile.GetFileRelativeDir());
//             if (!Directory.Exists(folderFull)) Directory.CreateDirectory(folderFull);
//             var filePath = Path.Combine(_appPreset!.FilesDir, entryFile.GetFileRelativePath());
//
//             await using (var stream = System.IO.File.Create(filePath))
//             {
//                 await formFile.CopyToAsync(stream, cancellationToken);
//             }
//
//             await _entryFileRepository.CreateAsync(entryFile, cancellationToken);
//         }
//
//         return Ok(new {count = request.Files.Count, size});
//     }
// }
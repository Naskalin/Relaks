// using App.DbConfigurations;
// using App.Models;
// using App.Utils.App;
// using Ardalis.ApiEndpoints;
// using Microsoft.AspNetCore.Mvc;
// using Swashbuckle.AspNetCore.Annotations;
//
// namespace App.Endpoints.Files;
//
// public class ExplorerFiles : EndpointBaseAsync
//     .WithRequest<Guid>
//     .WithActionResult
// {
//     private readonly AppDbContext _db;
//     private readonly AppPresetModel? _appPreset;
//
//     public ExplorerFiles(AppDbContext db, IConfiguration configuration)
//     {
//         _db = db;
//         _appPreset = AppPresetManager.GetPreset(configuration.GetValue<string>(WebHostDefaults.ContentRootKey));
//     }
//
//     [HttpGet("/api/files/{fileId:guid}/explorer")]
//     [SwaggerOperation(OperationId = "Explorer", Tags = new[] {"Files"})]
//     public override async Task<ActionResult> HandleAsync(
//         [FromRoute] Guid fileId,
//         CancellationToken cancellationToken = new()
//     )
//     {
//         var fileModel = await _db.FileModels.FindAsync(fileId);
//         if (fileModel == null)
//         {
//             return NotFound("File not found.");
//         }
//         
//         if (fileModel.Discriminator == nameof(EntryFile))
//         {
//             var entryFile = (EntryFile) fileModel;
//             var filePath = Path.Combine(_appPreset!.FilesDir, entryFile.GetFileRelativePath());
//             if (!System.IO.File.Exists(filePath))
//             {
//                 return NotFound("File path not exists.");
//             }
//
//             System.Diagnostics.Process.Start("explorer.exe", Path.Combine("select", filePath));
//         }
//         
//         return Ok();
//     }
// }
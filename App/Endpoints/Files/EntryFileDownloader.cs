using App.Models;
using App.Repository;
using App.Utils;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using Swashbuckle.AspNetCore.Annotations;

namespace App.Endpoints.Files;

public class EntryFileDownload : EndpointBaseAsync
    .WithRequest<FileRequest>
    .WithActionResult
{
    private readonly EntryFileRepository _entryFileRepository;
    private readonly AppPreset _appPreset;

    public EntryFileDownload(EntryFileRepository entryFileRepository, AppPreset appPreset)
    {
        _entryFileRepository = entryFileRepository;
        _appPreset = appPreset;
    }

    [HttpGet("/api/entryFiles/{fileId:guid}")]
    [SwaggerOperation(OperationId = "EntryFile", Tags = new[] {"Files"})]
    public override async Task<ActionResult> HandleAsync(
        [FromMultiSource] FileRequest req,
        CancellationToken cancellationToken = new()
    )
    {
        var entryFile = await _entryFileRepository.FindByIdAsync(req.FileId, cancellationToken);
        if (entryFile == null)
        {
            return NotFound("File not found.");
        }

        var filePath = Path.Combine(_appPreset.FilesDir, entryFile.GetFilePath());
        var streamOrigin = System.IO.File.OpenRead(filePath);
        
        if (entryFile.IsImage())
        {
            var imagePath = await GetImagePath(streamOrigin, entryFile, req.ImageFilter, cancellationToken);
            if (imagePath != null)
            {
                streamOrigin.Close();
                FileStream streamCacheImage = System.IO.File.OpenRead(imagePath);
                return new FileStreamResult(streamCacheImage, entryFile.ContentType);
            }
        }
        
        return new FileStreamResult(streamOrigin, entryFile.ContentType);
    }

    private async Task<string?> GetImagePath(
        FileStream fileOrigin,
        EntryFile entryFile,
        string? imageFilter,
        CancellationToken cancellationToken
    )
    {
        if (imageFilter == "thumbnail")
        {
            var fileCacheDir = Path.Combine(_appPreset.CacheDir, entryFile.GetFileDir(), imageFilter);
            var thumbnailPath = Path.Combine(fileCacheDir, entryFile.Path);
            if (!System.IO.File.Exists(thumbnailPath))
            {
                using (Image image = await Image.LoadAsync(fileOrigin, cancellationToken))
                {
                    if (!Directory.Exists(fileCacheDir)) Directory.CreateDirectory(fileCacheDir);
                    image.Mutate(
                        x => x.Resize(new ResizeOptions()
                        {
                            Mode = ResizeMode.Crop,
                            Size = new Size(50, 50)
                        })
                    );
                    await image.SaveAsync(thumbnailPath, cancellationToken);   
                }
            }
            
            return thumbnailPath;
        }

        return null;
    }
}
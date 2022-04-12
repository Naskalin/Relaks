using System.Net.Mime;
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

        Stream? stream = null;
        if (entryFile.IsImage() && !string.IsNullOrEmpty(req.ImageFilter))
        {
            switch (req.ImageFilter)
            {
                case "thumbnail":
                    var fileCacheDir = Path.Combine(_appPreset.CacheDir, entryFile.GetFileDir(), req.ImageFilter);
                    var thumbnailPath = Path.Combine(fileCacheDir, entryFile.Path);
                    if (!System.IO.File.Exists(thumbnailPath))
                    {
                        stream = System.IO.File.OpenRead(filePath);
                        using (Image image = await Image.LoadAsync(stream, cancellationToken))
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

                    stream = System.IO.File.OpenRead(thumbnailPath);

                    break;
                default:
                    return BadRequest("Image filter not found.");
            }
        }

        stream ??= System.IO.File.OpenRead(filePath);
        return new FileStreamResult(stream, entryFile.ContentType);
    }
}
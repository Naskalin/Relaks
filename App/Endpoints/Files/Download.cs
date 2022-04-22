using App.DbConfigurations;
using App.Models;
using App.Utils;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using Swashbuckle.AspNetCore.Annotations;

namespace App.Endpoints.Files;

public class DownloadFiles : EndpointBaseAsync
    .WithRequest<DownloadFilesRequest>
    .WithActionResult
{
    private readonly AppDbContext _db;
    private readonly AppPreset _appPreset;

    public DownloadFiles(AppDbContext db, AppPreset appPreset)
    {
        _db = db;
        _appPreset = appPreset;
    }

    [HttpGet("/api/files/{fileId:guid}/download")]
    [SwaggerOperation(OperationId = "Download", Tags = new[] {"Files"})]
    public override async Task<ActionResult> HandleAsync(
        [FromMultiSource] DownloadFilesRequest req,
        CancellationToken cancellationToken = new()
    )
    {
        var fileModel = await _db.FileModels.FindAsync(req.FileId);
        if (fileModel == null)
        {
            return NotFound("File not found.");
        }

        string fileRelativeDir;

        switch (fileModel.Discriminator)
        {
            case nameof(EntryFile):
                var entryFile = (EntryFile) fileModel;
                fileRelativeDir = entryFile.GetFileRelativeDir();
                break;
            case nameof(EntryInfoFile):
                var entryInfoFile = (EntryInfoFile) fileModel;
                fileRelativeDir = entryInfoFile.GetFileRelativeDir();
                break;
            default:
                return BadRequest("File type not found.");
        }

        var dirFull = Path.Combine(_appPreset.FilesDir, fileRelativeDir);
        var fileFullPath = Path.Combine(dirFull, fileModel.Path);
        var fileStream = System.IO.File.OpenRead(fileFullPath);

        if (fileModel.IsImage())
        {
            var imagePath = await TryApplyImageFilter(
                fileStream,
                fileRelativeDir,
                fileModel.Path,
                fileModel.ContentType,
                req.ImageFilter,
                cancellationToken
            );

            if (imagePath != null)
            {
                fileStream.Close();
                fileStream = System.IO.File.OpenRead(imagePath);
            }
        }

        var fileDownloadName = fileModel.Name + Path.GetExtension(fileModel.Path);
        return new FileStreamResult(fileStream, fileModel.ContentType)
        {
            FileDownloadName = fileDownloadName
        };
    }

    private async Task<string?> TryApplyImageFilter(
        FileStream fileOrigin,
        string fileRelativeDir,
        string filePath,
        string contentType,
        string? imageFilter,
        CancellationToken cancellationToken
    )
    {
        var config = Configuration.Default;
        var isSupportedImageExtension = config.ImageFormats
                .Where(x => config.ImageFormatsManager.FindDecoder(x) != null)
                .SelectMany(x => x.MimeTypes)
                .Contains(contentType)
            ;
        if (!isSupportedImageExtension) return null;

        if (imageFilter == "square-thumbnail")
        {
            var fileCacheDir = Path.Combine(_appPreset.CacheDir, fileRelativeDir, imageFilter);
            var thumbnailPath = Path.Combine(fileCacheDir, filePath);
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
        
        if (imageFilter == "square-medium")
        {
            var fileCacheDir = Path.Combine(_appPreset.CacheDir, fileRelativeDir, imageFilter);
            var thumbnailPath = Path.Combine(fileCacheDir, filePath);
            if (!System.IO.File.Exists(thumbnailPath))
            {
                using (Image image = await Image.LoadAsync(fileOrigin, cancellationToken))
                {
                    if (!Directory.Exists(fileCacheDir)) Directory.CreateDirectory(fileCacheDir);
                    image.Mutate(
                        x => x.Resize(new ResizeOptions()
                        {
                            Mode = ResizeMode.Crop,
                            Size = new Size(200, 200)
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
using App.DbConfigurations;
using App.Utils.App;
using Microsoft.AspNetCore.Authorization;
using App.Models;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace App.Endpoints.Files;

[HttpGet("/api/files/{fileId:guid}/download"), AllowAnonymous]
public class DownloadFiles : Endpoint<DownloadFilesRequest>
{
    private readonly AppDbContext _db;
    private AppPresetFull? _appPreset;

    public DownloadFiles(AppDbContext db)
    {
        _db = db;
    }

    public override async Task HandleAsync(DownloadFilesRequest req, CancellationToken ct)
    {
        _appPreset = AppPresetManager.GetPreset(Config.GetValue<string>(WebHostDefaults.ContentRootKey));
        var fileModel = await _db.FileModels.FindAsync(req.FileId);
        if (fileModel == null)
        {
            await SendNotFoundAsync(ct);
            return;
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
                ThrowError("file type not found");
                return;
        }

        var dirFull = Path.Combine(_appPreset!.FilesDir, fileRelativeDir);
        var fileFullPath = Path.Combine(dirFull, fileModel.Path);
        var fileStream = File.OpenRead(fileFullPath);

        if (fileModel.IsImage())
        {
            var imagePath = await TryApplyImageFilter(
                fileStream,
                fileRelativeDir,
                fileModel.Path,
                fileModel.ContentType,
                req.ImageFilter,
                ct
            );

            if (imagePath != null)
            {
                fileStream.Close();
                fileStream = File.OpenRead(imagePath);
            }
        }

        var fileDownloadName = fileModel.Name + Path.GetExtension(fileModel.Path);

        await SendStreamAsync(
            stream: fileStream,
            fileName: fileDownloadName,
            fileLengthBytes: fileStream.Length,
            contentType: fileModel.ContentType,
            cancellation: ct
        );
        // return new FileStreamResult(fileStream, fileModel.ContentType)
        // {
        //     FileDownloadName = fileDownloadName
        // };
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
            var fileCacheDir = Path.Combine(_appPreset!.CacheDir, fileRelativeDir, imageFilter);
            var thumbnailPath = Path.Combine(fileCacheDir, filePath);
            if (!File.Exists(thumbnailPath))
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
            var fileCacheDir = Path.Combine(_appPreset!.CacheDir, fileRelativeDir, imageFilter);
            var thumbnailPath = Path.Combine(fileCacheDir, filePath);
            if (!File.Exists(thumbnailPath))
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
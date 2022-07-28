using App.DbConfigurations;
using App.Models;
using App.Utils.App;
using Microsoft.AspNetCore.Authorization;

namespace App.Endpoints.Files;

[HttpGet("/api/files/{fileId:guid}/explorer"), AllowAnonymous]
public class ExplorerFiles : Endpoint<ExplorerFilesRequest>
{
    private readonly AppDbContext _db;
    private readonly AppPresetFull? _appPreset;

    public ExplorerFiles(AppDbContext db)
    {
        _db = db;
        _appPreset = AppPresetManager.GetPreset(Config.GetValue<string>(WebHostDefaults.ContentRootKey));
    }

    public override async Task HandleAsync(ExplorerFilesRequest req, CancellationToken ct)
    {
        var fileModel = await _db.FileModels.FindAsync(req.FileId);
        if (fileModel == null)
        {
            ThrowError("File not found");
            return;
        }
        
        if (fileModel.Discriminator == nameof(EntryFile))
        {
            var entryFile = (EntryFile) fileModel;
            var filePath = Path.Combine(_appPreset!.FilesDir, entryFile.GetFileRelativePath());
            if (!File.Exists(filePath))
            {
                ThrowError("File path not exists");
                return;
            }

            System.Diagnostics.Process.Start("explorer.exe", Path.Combine("select", filePath));
        }
        
        await SendNoContentAsync(ct);
    }
}
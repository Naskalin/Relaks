using App.Mappers;
using App.Models;
using App.Repository;
using App.Utils.App;
using Microsoft.AspNetCore.Authorization;

namespace App.Endpoints.EntryFiles;

[HttpPost("/api/entries/{entryId:guid}/files"), AllowAnonymous]
public class Create : Endpoint<EntryFileCreateRequest>
{
    private readonly EntryFileRepository _entryFileRepository;
    private readonly EntryRepository _entryRepository;

    public Create(EntryFileRepository entryFileRepository, EntryRepository entryRepository)
    {
        _entryFileRepository = entryFileRepository;
        _entryRepository = entryRepository;
    }

    public override async Task HandleAsync(EntryFileCreateRequest req, CancellationToken ct)
    {
        var appPreset = AppPresetManager.GetPreset(Config.GetValue<string>(WebHostDefaults.ContentRootKey));
        var entry = await _entryRepository.FindByIdAsync(req.EntryId, ct);
        if (entry == null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        var size = req.Files.Sum(f => f.Length);
        var category = "";
        if (req.Category != null) category = req.Category.Trim();
        
        foreach (var formFile in req.Files)
        {
            if (formFile.Length <= 0) continue;

            var entryFile = new EntryFile
            {
                EntryId = entry.Id,
                Category = category
            };
            formFile.MapToCreate(entryFile);

            var folderFull = Path.Combine(appPreset!.FilesDir, entryFile.GetFileRelativeDir());
            if (!Directory.Exists(folderFull)) Directory.CreateDirectory(folderFull);
            var filePath = Path.Combine(appPreset!.FilesDir, entryFile.GetFileRelativePath());

            await using (var stream = File.Create(filePath))
            {
                await formFile.CopyToAsync(stream, ct);
            }

            await _entryFileRepository.CreateAsync(entryFile, ct);
        }

        await SendOkAsync(new {count = req.Files.Count, size}, ct);
    }
}
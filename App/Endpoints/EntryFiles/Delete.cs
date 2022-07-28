using App.Repository;
using App.Utils.App;
using Microsoft.AspNetCore.Authorization;

namespace App.Endpoints.EntryFiles;

[HttpDelete("/api/entries/{entryId:guid}/files/{entryFileId:guid}"), AllowAnonymous]
public class Delete : Endpoint<EntryFileGetRequest>
{
    private readonly EntryFileRepository _entryFileRepository;
    private readonly EntryRepository _entryRepository;

    public Delete(EntryFileRepository entryFileRepository, EntryRepository entryRepository)
    {
        _entryFileRepository = entryFileRepository;
        _entryRepository = entryRepository;
    }

    public override async Task HandleAsync(EntryFileGetRequest req, CancellationToken ct)
    {
        var appPreset = AppPresetManager.GetPreset(Config.GetValue<string>(WebHostDefaults.ContentRootKey));
        var entryFile = await _entryFileRepository.FindByIdAsync(req.EntryFileId, ct);
        if (entryFile == null || entryFile.EntryId != req.EntryId)
        {
            await SendNotFoundAsync(ct);
            return;
        }
        
        await _entryFileRepository.DeleteAsync(entryFile, ct);
        
        // Remove attached file
        var filePath = Path.Combine(appPreset!.FilesDir, entryFile.GetFileRelativePath());
        if (File.Exists(filePath)) File.Delete(filePath);
        
        // Remove if its avatar
        var entry = await _entryRepository.FindByIdAsync(req.EntryId, ct);
        if (entry!.Avatar == entryFile.Id)
        {
            entry.Avatar = null;
            await _entryRepository.UpdateAsync(entry, ct);
        }

        await SendNoContentAsync(ct);
    }
}
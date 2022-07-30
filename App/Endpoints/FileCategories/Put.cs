using App.Mappers;
using App.Repository;
using Microsoft.AspNetCore.Authorization;

namespace App.Endpoints.FileCategories;

[HttpPut("/api/file-categories/{fileCategoryId:guid}"), AllowAnonymous]
public class Put : Endpoint<FileCategoryPutRequest>
{
    private readonly FileCategoryRepository _fileCategoryRepository;

    public Put(FileCategoryRepository fileCategoryRepository)
    {
        _fileCategoryRepository = fileCategoryRepository;
    }
    
    public override async Task HandleAsync(FileCategoryPutRequest req, CancellationToken ct)
    {
        var fc = await _fileCategoryRepository.FindByIdAsync(req.FileCategoryId, ct);
        if (fc == null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        req.MapTo(fc);
        await _fileCategoryRepository.UpdateAsync(fc, ct);
        await SendNoContentAsync(ct);
    }
}
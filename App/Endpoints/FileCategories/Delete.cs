using App.Repository;
using Microsoft.AspNetCore.Authorization;

namespace App.Endpoints.FileCategories;

[HttpDelete("/api/file-categories/{fileCategoryId:guid}"), AllowAnonymous]
public class Delete : Endpoint<FileCategoryGetRequest>
{
    private readonly FileCategoryRepository _fileCategoryRepository;

    public Delete(FileCategoryRepository fileCategoryRepository)
    {
        _fileCategoryRepository = fileCategoryRepository;
    }
    
    public override async Task HandleAsync(FileCategoryGetRequest req, CancellationToken ct)
    {
        var fc = await _fileCategoryRepository.FindByIdAsync(req.FileCategoryId, ct);
        if (fc == null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        await _fileCategoryRepository.DeleteAsync(fc, ct);
        await SendNoContentAsync(ct);
    }
}
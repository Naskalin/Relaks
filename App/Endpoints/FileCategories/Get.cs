using App.Repository;
using Microsoft.AspNetCore.Authorization;

namespace App.Endpoints.FileCategories;

[HttpGet("/api/file-categories/{fileCategoryId:guid}"), AllowAnonymous]
public class Get : Endpoint<FileCategoryGetRequest>
{
    private readonly FileCategoryRepository _fileCategoryRepository;

    public Get(FileCategoryRepository fileCategoryRepository)
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

        await SendOkAsync(fc, ct);
    }
}
using App.Models;
using App.Repository;
using App.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace App.Endpoints.FileCategories;

[HttpGet("/api/file-categories"), AllowAnonymous]
public class List : Endpoint<FileCategoryListRequest>
{
    private readonly FileCategoryRepository _fileCategoryRepository;

    public List(FileCategoryRepository fileCategoryRepository)
    {
        _fileCategoryRepository = fileCategoryRepository;
    }
    
    public override async Task HandleAsync(FileCategoryListRequest req, CancellationToken ct)
    {
        var query = _fileCategoryRepository.FindForListRequest(req);

        var items = await query
            .Cast<EntryFileCategory>()
            .Where(x => x.EntryId.Equals(req.EntryId)).ToListAsync(ct);
        
        if (req.IsTree is true)
        {
            await SendOkAsync(items.ToTree((parent, child) => child.ParentId == parent.Id), ct);
            return;
        }

        await SendOkAsync(items, ct);
    }
}
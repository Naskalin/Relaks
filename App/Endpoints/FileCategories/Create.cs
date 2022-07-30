using App.Mappers;
using App.Models;
using App.Repository;
using Microsoft.AspNetCore.Authorization;

namespace App.Endpoints.FileCategories;

[HttpPost("/api/file-categories"), AllowAnonymous]
public class Create : Endpoint<FileCategoryCreateRequest>
{
    private readonly FileCategoryRepository _fileCategoryRepository;
    private readonly EntryRepository _entryRepository;

    public Create(FileCategoryRepository fileCategoryRepository, EntryRepository entryRepository)
    {
        _fileCategoryRepository = fileCategoryRepository;
        _entryRepository = entryRepository;
    }

    public override async Task HandleAsync(FileCategoryCreateRequest req, CancellationToken ct)
    {
        var entry = await _entryRepository.FindByIdAsync(req.EntryId, ct);
        if (entry == null) ThrowError(x => x.EntryId, "Entry not found");
        
        var fileCategory = new EntryFileCategory()
        {
            EntryId = req.EntryId,
            CreatedAt = DateTime.UtcNow,
        };
        
        req.MapTo(fileCategory);
        await _fileCategoryRepository.CreateAsync(fileCategory, ct);
        await SendCreatedAtAsync<Get>(new {fileCategoryId = fileCategory.Id}, fileCategory, cancellation: ct);
    }
}
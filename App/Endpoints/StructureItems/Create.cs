using App.Mappers;
using App.Models;
using App.Repository;
using Microsoft.AspNetCore.Authorization;

namespace App.Endpoints.StructureItems;

[HttpPost("/api/structure-items"), AllowAnonymous]
public class Create : Endpoint<StructureItemCreateRequest>
{
    private readonly StructureItemRepository _structureItemRepository;
    private readonly StructureItemDbValidate _structureItemDbValidate;

    public Create(
        StructureItemRepository structureItemRepository,
        StructureItemDbValidate structureItemDbValidate
        )
    {
        _structureItemRepository = structureItemRepository;
        _structureItemDbValidate = structureItemDbValidate;
    }

    
    public override async Task HandleAsync(StructureItemCreateRequest req, CancellationToken ct)
    {
        var errors = await _structureItemDbValidate.ValidateAsync(req, ct);
        if (errors.Any())
        {
            errors.ForEach(e => AddError(x => e.PropertyName, e.ErrorMessage));
        };
        
        ThrowIfAnyErrors();

        var structureItem = new StructureItem()
        {
            CreatedAt = DateTime.UtcNow,
        };
        req.MapTo(structureItem);
        await _structureItemRepository.CreateAsync(structureItem, ct);
        await SendCreatedAtAsync<Get>(new {structureItemId = structureItem.Id}, structureItem, cancellation: ct);
    }
}
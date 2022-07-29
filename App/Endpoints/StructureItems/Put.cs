using App.Mappers;
using App.Repository;
using Microsoft.AspNetCore.Authorization;

namespace App.Endpoints.StructureItems;

[HttpPut("/api/structure-items/{structureItemId:guid}"), AllowAnonymous]
public class Put : Endpoint<StructureItemPutRequest>
{
    private readonly StructureItemDbValidate _structureItemDbValidate;
    private readonly StructureItemRepository _structureItemRepository;

    public Put(StructureItemDbValidate structureItemDbValidate, StructureItemRepository structureItemRepository)
    {
        _structureItemDbValidate = structureItemDbValidate;
        _structureItemRepository = structureItemRepository;
    }

    public override async Task HandleAsync(StructureItemPutRequest req, CancellationToken ct)
    {
        var errors = await _structureItemDbValidate.ValidateAsync(req, ct);
        if (errors.Any())
        {
            errors.ForEach(e => AddError(e.PropertyName, e.ErrorMessage));
            ThrowIfAnyErrors();
            return;
        };

        var structureItem = await _structureItemRepository.FindByIdAsync(req.StructureItemId, ct);
        if (structureItem == null)
        {
            await SendNotFoundAsync(ct);
            return;
        }
        
        req.MapTo(structureItem);
        await _structureItemRepository.UpdateAsync(structureItem, ct);
        await SendNoContentAsync(ct);
    }
}
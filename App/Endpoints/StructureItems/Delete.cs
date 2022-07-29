using App.Repository;
using Microsoft.AspNetCore.Authorization;

namespace App.Endpoints.StructureItems;

[HttpDelete("/api/structure-items/{structureItemId:guid}"), AllowAnonymous]
public class Delete : Endpoint<StructureItemGetRequest>
{
    private readonly StructureItemRepository _structureItemRepository;

    public Delete(StructureItemRepository structureItemRepository)
    {
        _structureItemRepository = structureItemRepository;
    }

    
    public override async Task HandleAsync(StructureItemGetRequest req, CancellationToken ct)
    {
        var structureItem = await _structureItemRepository.FindByIdAsync(req.StructureItemId, ct);
        if (structureItem == null)
        {
            await SendNotFoundAsync(ct);
            return;
        }
        await _structureItemRepository.DeleteAsync(structureItem, ct);
        await SendNoContentAsync(ct);
    }
}
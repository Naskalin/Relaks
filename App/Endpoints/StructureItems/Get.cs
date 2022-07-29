using App.Repository;
using Microsoft.AspNetCore.Authorization;

namespace App.Endpoints.StructureItems;

[HttpGet("/api/structure-items/{structureItemId:guid}"), AllowAnonymous]
public class Get : Endpoint<StructureItemGetRequest>
{
    private readonly StructureItemRepository _structureItemRepository;

    public Get(StructureItemRepository structureItemRepository)
    {
        _structureItemRepository = structureItemRepository;
    }
    
    public override async Task HandleAsync(StructureItemGetRequest req, CancellationToken ct)
    {
        var structureItem = await _structureItemRepository.FindByIdWithRelationsAsync(req.StructureItemId, ct);
        if (structureItem == null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        await SendOkAsync(structureItem, ct);
    }
}
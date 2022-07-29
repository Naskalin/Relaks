using App.Repository;
using Microsoft.AspNetCore.Authorization;

namespace App.Endpoints.StructureItems;

[HttpGet("/api/structure-items"), AllowAnonymous]
public class List : Endpoint<StructureItemListRequest>
{
    private readonly StructureItemRepository _structureItemRepository;

    public List(StructureItemRepository structureItemRepository)
    {
        _structureItemRepository = structureItemRepository;
    }
    
    public override async Task HandleAsync(StructureItemListRequest req, CancellationToken ct)
    {
        if (req.EntryId == null && req.StructureId == null)
        {
            ThrowError("entryId or structureId must be in request");
            return;
        }

        var structureItems = await _structureItemRepository.FindByListRequestAsync(req, ct);
        await SendOkAsync(structureItems, ct);
    }
}
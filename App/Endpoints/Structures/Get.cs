using App.Repository;
using Microsoft.AspNetCore.Authorization;

namespace App.Endpoints.Structures;

[HttpGet("/api/entries/{entryId:guid}/structures/{structureId:guid}"), AllowAnonymous]
public class Get : Endpoint<StructureGetRequest>
{
    private readonly StructureRepository _structureRepository;

    public Get(StructureRepository structureRepository)
    {
        _structureRepository = structureRepository;
    }
    
    public override async Task HandleAsync(StructureGetRequest request, CancellationToken ct)
    {
        var structure = await _structureRepository.FindByIdAsync(request.StructureId, ct);
        if (structure == null || structure.EntryId != request.EntryId)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        await SendOkAsync(structure, ct);
    }
}
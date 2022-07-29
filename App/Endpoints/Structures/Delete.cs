using App.Repository;
using Microsoft.AspNetCore.Authorization;

namespace App.Endpoints.Structures;

[HttpDelete("/api/entries/{entryId:guid}/structures/{structureId:guid}"), AllowAnonymous]
public class Delete : Endpoint<StructureGetRequest>
{
    private readonly StructureRepository _structureRepository;

    public Delete(StructureRepository structureRepository)
    {
        _structureRepository = structureRepository;
    }

    public override async Task HandleAsync(StructureGetRequest req, CancellationToken ct)
    {
        var structure = await _structureRepository.FindByIdAsync(req.StructureId, ct);
        if (structure == null || structure.EntryId != req.EntryId)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        await _structureRepository.DeleteAsync(structure, ct);
        await SendNoContentAsync(ct);
    }
}
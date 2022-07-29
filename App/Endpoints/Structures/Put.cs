using App.Mappers;
using App.Repository;
using Microsoft.AspNetCore.Authorization;

namespace App.Endpoints.Structures;

[HttpPut("/api/entries/{entryId:guid}/structures/{structureId:guid}"), AllowAnonymous]
public class Put : Endpoint<StructurePutRequest>
{
    public override void OnBeforeHandle(StructurePutRequest req)
    {
        
    }

    private readonly StructureRepository _structureRepository;

    public Put(StructureRepository structureRepository)
    {
        _structureRepository = structureRepository;
    }

    public override async Task HandleAsync(StructurePutRequest req, CancellationToken ct)
    {
        var structure = await _structureRepository.FindByIdAsync(req.StructureId, ct);
        if (structure == null || structure.EntryId != req.EntryId)
        {
            await SendNotFoundAsync(ct);
            return;
        }
        
        if (req.StructureId.Equals(req.ParentId))
        {
            ThrowError(x => x.StructureId, "Группа не может быть родительской для самой себя.");
        }
        
        req.MapTo(structure);
        
        await _structureRepository.UpdateAsync(structure, ct);
        await SendNoContentAsync(ct);
    }
}
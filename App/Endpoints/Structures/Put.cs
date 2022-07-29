using App.Mappers;
using App.Repository;
using Microsoft.AspNetCore.Authorization;

namespace App.Endpoints.Structures;

[HttpPut("/api/entries/{entryId:guid}/structures/{structureId:guid}"), AllowAnonymous]
public class Put : Endpoint<StructurePutRequest>
{
    private readonly StructureRepository _structureRepository;

    public Put(StructureRepository structureRepository)
    {
        _structureRepository = structureRepository;
    }
    
    public override async Task HandleAsync(StructurePutRequest req, CancellationToken ct)
    {
        if (req.StructureId.Equals(req.ParentId))
        {
            AddError("StructureId", "Группа не может быть родительской для самой себя.");
            ThrowIfAnyErrors();
        }

        var structure = await _structureRepository.FindByIdAsync(req.StructureId, ct);
        if (structure == null || structure.EntryId != req.EntryId)
        {
            await SendNotFoundAsync(ct);
            return;
        }
        
        req.MapTo(structure);
        
        await _structureRepository.UpdateAsync(structure, ct);
        await SendNoContentAsync(ct);
    }
}
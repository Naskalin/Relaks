using App.Mappers;
using App.Models;
using App.Repository;
using Microsoft.AspNetCore.Authorization;

namespace App.Endpoints.Structures;

[HttpPost("/api/entries/{entryId:guid}/structures"), AllowAnonymous]
public class Create : Endpoint<StructureCreateRequest>
{
    private readonly StructureRepository _structureRepository;

    public Create(StructureRepository structureRepository)
    {
        _structureRepository = structureRepository;
    }

    public override async Task HandleAsync(StructureCreateRequest request, CancellationToken ct)
    {
        var structure = new Structure()
        {
            EntryId = request.EntryId,
            CreatedAt = DateTime.UtcNow
        };
        request.MapTo(structure);
        await _structureRepository.CreateAsync(structure, ct);
        await SendCreatedAtAsync<Get>(
            new {entryId = request.EntryId, structureId = structure.Id},
            structure,
            cancellation: ct
        );
    }
}
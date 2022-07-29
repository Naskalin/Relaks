using App.Mappers;
using App.Models;
using App.Repository;
using Microsoft.AspNetCore.Authorization;

namespace App.Endpoints.StructureConnections;

[HttpPost("/api/structure-connections"), AllowAnonymous]
public class Create : Endpoint<StructureConnectionCreateRequest>
{
    private readonly StructureConnectionRepository _structureConnectionRepository;

    public Create(StructureConnectionRepository structureConnectionRepository)
    {
        _structureConnectionRepository = structureConnectionRepository;
    }

    public override async Task HandleAsync(StructureConnectionCreateRequest req, CancellationToken ct)
    {
        var structureConnection = new StructureConnection() {CreatedAt = DateTime.UtcNow};
        req.MapTo(structureConnection);
        await _structureConnectionRepository.CreateAsync(structureConnection, ct);
        await SendCreatedAtAsync<Get>(
            new {structureConnectionId = structureConnection.Id},
            structureConnection,
            cancellation: ct
        );
    }
}
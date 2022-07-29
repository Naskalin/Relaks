using App.Mappers;
using App.Repository;
using Microsoft.AspNetCore.Authorization;

namespace App.Endpoints.StructureConnections;

[HttpPut("/api/structure-connections/{structureConnectionId:guid}"), AllowAnonymous]
public class Put : Endpoint<StructureConnectionPutRequest>
{
    private readonly StructureConnectionRepository _structureConnectionRepository;

    public Put(StructureConnectionRepository structureConnectionRepository)
    {
        _structureConnectionRepository = structureConnectionRepository;
    }

    public override async Task HandleAsync(StructureConnectionPutRequest req, CancellationToken ct)
    {
        var sConnection = await _structureConnectionRepository
            .FindByIdAsync(req.StructureConnectionId, ct);
        if (sConnection == null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        req.MapTo(sConnection);
        await _structureConnectionRepository.UpdateAsync(sConnection, ct);
        await SendNoContentAsync(ct);
    }
}
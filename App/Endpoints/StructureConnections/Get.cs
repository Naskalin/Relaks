using App.Repository;
using Microsoft.AspNetCore.Authorization;

namespace App.Endpoints.StructureConnections;

[HttpGet("/api/structure-connections/{structureConnectionId:guid}"), AllowAnonymous]
public class Get : Endpoint<StructureConnectionGetRequest>
{
    private readonly StructureConnectionRepository _structureConnectionRepository;

    public Get(StructureConnectionRepository structureConnectionRepository)
    {
        _structureConnectionRepository = structureConnectionRepository;
    }

    public override async Task HandleAsync(StructureConnectionGetRequest req, CancellationToken ct)
    {
        var structureConnection = await _structureConnectionRepository
            .FindByIdAsync(req.StructureConnectionId, ct);
        if (structureConnection == null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        await SendOkAsync(structureConnection, ct);
    }
}
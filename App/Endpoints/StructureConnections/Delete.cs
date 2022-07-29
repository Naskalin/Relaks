using App.Repository;
using Microsoft.AspNetCore.Authorization;

namespace App.Endpoints.StructureConnections;

[HttpDelete("/api/structure-connections/{structureConnectionId:guid}"), AllowAnonymous]
public class Delete : Endpoint<StructureConnectionGetRequest>
{
    private readonly StructureConnectionRepository _structureConnectionRepository;

    public Delete(StructureConnectionRepository structureConnectionRepository)
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

        await _structureConnectionRepository.DeleteAsync(structureConnection, ct);
        await SendNoContentAsync(ct);
    }
}
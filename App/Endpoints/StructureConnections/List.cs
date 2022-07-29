using App.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace App.Endpoints.StructureConnections;

[HttpGet("/api/structure-connections"), AllowAnonymous]
public class List : Endpoint<StructureConnectionListRequest>

{
    private readonly StructureConnectionRepository _structureConnectionRepository;

    public List(StructureConnectionRepository structureConnectionRepository)
    {
        _structureConnectionRepository = structureConnectionRepository;
    }

    public override async Task HandleAsync(StructureConnectionListRequest req, CancellationToken ct)
    {
        var items = await _structureConnectionRepository
                .FindByListRequest(req)
                .ToListAsync(ct)
            ;

        await SendOkAsync(items, ct);
    }
}
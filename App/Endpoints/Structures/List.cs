using App.Repository;
using App.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace App.Endpoints.Structures;

[HttpGet("/api/entries/{entryId:guid}/structures"), AllowAnonymous]
public class List : Endpoint<StructureListRequest>
{
    private readonly StructureRepository _structureRepository;

    public List(StructureRepository structureRepository)
    {
        _structureRepository = structureRepository;
    }

    public override async Task HandleAsync(StructureListRequest req, CancellationToken ct)
    {
        var structures = await _structureRepository.FindStructures(req).ToListAsync(ct);
        if (req.IsTree == true)
        {
            await SendOkAsync(structures.ToTree((parent, child) => child.ParentId == parent.Id), ct);
            return;
        }

        await SendOkAsync(structures, ct);
    }
}
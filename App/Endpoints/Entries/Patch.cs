using App.Mappers;
using App.Repository;
using App.Utils;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace App.Endpoints.Entries;

public class Patch : EndpointBaseAsync
    .WithRequest<PatchRequest>
    .WithoutResult
{
    private readonly EntryRepository _entryRepository;

    public Patch(EntryRepository entryRepository)
    {
        _entryRepository = entryRepository;
    }

    [HttpPatch("/api/entries/{id}")]
    public override async Task<ActionResult> HandleAsync(
        [FromMultiSource] PatchRequest patchRequest,
        CancellationToken cancellationToken = new())
    {
        var entry = await _entryRepository.FindSingleAsync(patchRequest.Id);
        if (entry == null) return NotFound();
        patchRequest.MapTo(entry);
        
        _entryRepository.Update(entry);
        await _entryRepository.SaveChangesAsync();
    
        return NoContent();
    }
    // public override Task HandleAsync(PatchRequest request, CancellationToken cancellationToken = new CancellationToken())
    // {
    //     throw new NotImplementedException();
    // }
}
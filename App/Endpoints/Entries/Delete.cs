using App.Mappers;
using App.Repository;
using App.Utils;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace App.Endpoints.Entries;

public class Delete : EndpointBaseAsync
    .WithRequest<DeleteRequest>
    .WithActionResult
{
    private readonly EntryRepository _entryRepository;

    public Delete(EntryRepository entryRepository)
    {
        _entryRepository = entryRepository;
    }

    [HttpDelete("/api/entries/{entryId}")]
    [SwaggerOperation(OperationId = "Entry.Delete", Tags = new[] {"Entry"})]
    public override async Task<ActionResult> HandleAsync(
        [FromMultiSource] DeleteRequest req,
        CancellationToken cancellationToken = new())
    {
        var entry = await _entryRepository.FindByIdAsync(req.EntryId, cancellationToken);
        if (entry == null)
        {
            return NotFound();
        }

        if (req.IsFullDelete == true)
        {
            await _entryRepository.DeleteAsync(entry, cancellationToken);
        }
        else
        {
            req.MapTo(entry);
            await _entryRepository.UpdateAsync(entry, cancellationToken);   
        }

        return NoContent();
    }
}
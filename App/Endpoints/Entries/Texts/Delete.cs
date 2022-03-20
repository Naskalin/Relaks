using App.Repository;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace App.Endpoints.Entries.Texts;

public class Delete : EndpointBaseAsync
    .WithRequest<GetRequest>
    .WithActionResult
{
    private readonly EntryTextRepository _entryTextRepository;

    public Delete(EntryTextRepository entryTextRepository)
    {
        _entryTextRepository = entryTextRepository;
    }

    [HttpDelete("/api/entries/{EntryId}/texts/{EntryTextId}")]
    [SwaggerOperation(OperationId = "EntryText.Delete", Tags = new[] {"EntryText"})]
    public override async Task<ActionResult> HandleAsync(
        [FromRoute] GetRequest request,
        CancellationToken cancellationToken = new())
    {
        var entryText = await _entryTextRepository.FindByIdAsync(request.EntryTextId, cancellationToken);
        if (entryText == null || entryText.EntryId != request.EntryId)
        {
            return NotFound();
        }

        await _entryTextRepository.DeleteAsync(entryText, cancellationToken);
        return NoContent();
    }
}
using App.Repository;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace App.Endpoints.Entries.Dates;

public class Delete : EndpointBaseAsync
    .WithRequest<GetRequest>
    .WithActionResult
{
    private readonly EntryDateRepository _entryDateRepository;

    public Delete(EntryDateRepository entryDateRepository)
    {
        _entryDateRepository = entryDateRepository;
    }

    [HttpDelete("/api/entries/{EntryId}/dates/{EntryDateId}")]
    [SwaggerOperation(OperationId = "EntryDate.Delete", Tags = new[] {"EntryDate"})]
    public override async Task<ActionResult> HandleAsync(
        [FromRoute] GetRequest request,
        CancellationToken cancellationToken = new())
    {
        var entryDate = await _entryDateRepository.FindByIdAsync(request.EntryDateId, cancellationToken);
        if (entryDate == null || entryDate.EntryId != request.EntryId)
        {
            return NotFound();
        }

        await _entryDateRepository.DeleteAsync(entryDate, cancellationToken);
        return NoContent();
    }
}
using App.Mappers;
using App.Repository;
using App.Utils;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace App.Endpoints.Entries.Dates;

public class Put : EndpointBaseAsync
    .WithRequest<PutRequest>
    .WithActionResult

{
    private readonly EntryDateRepository _entryDateRepository;

    public Put(EntryDateRepository entryDateRepository)
    {
        _entryDateRepository = entryDateRepository;
    }

    [HttpPut("/api/entries/{EntryId}/dates/{EntryDateId}")]
    [SwaggerOperation(OperationId = "EntryDate.Put", Tags = new[] {"EntryDate"})]
    public override async Task<ActionResult> HandleAsync(
        [FromMultiSource] PutRequest request,
        CancellationToken cancellationToken = new())
    {
        var validation = await new CreateRequestValidator().ValidateAsync(request.Details, cancellationToken);
        if (!validation.IsValid)
        {
            return BadRequest(validation);
        }
        
        var entryDate = await _entryDateRepository.FindByIdAsync(request.EntryDateId, cancellationToken);
        if (entryDate == null || entryDate.EntryId != request.EntryId)
        {
            return NotFound();
        }
        
        request.MapTo(entryDate);
        entryDate.UpdatedAt = DateTime.UtcNow;
        await _entryDateRepository.UpdateAsync(entryDate, cancellationToken);

        return NoContent();
    }
}
using App.Models;
using App.Repository;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace App.Endpoints.Entries.Texts;

public class Get : EndpointBaseAsync
    .WithRequest<GetRequest>
    .WithActionResult<EntryText>
{
    private readonly EntryTextRepository _entryTextRepository;

    public Get(EntryTextRepository entryTextRepository)
    {
        _entryTextRepository = entryTextRepository;
    }

    [HttpGet("/api/entries/{EntryId}/texts/{EntryTextId}", Name = "Entries_Texts_Get")]
    [SwaggerOperation(OperationId = "EntryText.Get", Tags = new[] {"EntryText"})]
    public override async Task<ActionResult<EntryText>> HandleAsync(
        [FromRoute] GetRequest request,
        CancellationToken cancellationToken = new())
    {
        var entryText = await _entryTextRepository.FindByIdAsync(request.EntryTextId, cancellationToken);
        if (entryText == null || entryText.EntryId != request.EntryId)
        {
            return NotFound();
        }

        return Ok(entryText);
    }
}
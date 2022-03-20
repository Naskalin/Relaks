using App.Models;
using App.Repository;
using App.Utils;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace App.Endpoints.Entries.Dates;

public class List : EndpointBaseAsync
    .WithRequest<ListRequest>
    .WithActionResult<List<EntryDate>>
{
    private readonly EntryDateRepository _entryDateRepository;
    private readonly EntryRepository _entryRepository;

    public List(EntryDateRepository entryDateRepository, EntryRepository entryRepository)
    {
        _entryDateRepository = entryDateRepository;
        _entryRepository = entryRepository;
    }

    [HttpGet("/api/entries/{EntryId}/dates")]
    [SwaggerOperation(OperationId = "EntryDate.List", Tags = new[] {"EntryDate"})]
    public override async Task<ActionResult<List<EntryDate>>> HandleAsync(
        [FromMultiSource] ListRequest listRequest,
        CancellationToken cancellationToken = new())
    {
        var entry = await _entryRepository.FindByIdAsync(listRequest.EntryId, cancellationToken);
        if (entry == null)
        {
            return NotFound();
        }
        
        var dates = await _entryDateRepository.PaginateListAsync(listRequest, cancellationToken);
        return Ok(dates);
    }
}
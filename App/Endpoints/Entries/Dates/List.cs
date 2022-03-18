using App.Models;
using App.Repository;
using App.Utils;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;

namespace App.Endpoints.Entries.Dates;

public class List : EndpointBaseAsync
    .WithRequest<ListRequest>
    .WithActionResult<List<EntryDate>>
{
    private readonly EntryDateRepository _repository;

    public List(EntryDateRepository repository)
    {
        _repository = repository;
    }

    [HttpGet("/api/entries/{EntryId}/dates")]
    public override async Task<ActionResult<List<EntryDate>>> HandleAsync(
        [FromMultiSource] ListRequest listRequest,
        CancellationToken cancellationToken = new())
    {
        var dates = await _repository.PaginateListAsync(listRequest, cancellationToken);
        return Ok(dates);
    }
}
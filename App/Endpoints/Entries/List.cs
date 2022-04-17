using App.Models;
using App.Repository;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace App.Endpoints.Entries;

public class List : EndpointBaseAsync
    .WithRequest<ListRequest>
    // .WithActionResult<List<EntryFtsDto>>
    .WithActionResult
{
    private readonly EntryRepository _entryRepository;

    public List(EntryRepository entryRepository)
    {
        _entryRepository = entryRepository;
    }

    [HttpGet("/api/entries")]
    [SwaggerOperation(OperationId = "Entry.List", Tags = new[] {"Entry"})]
    // public override async Task<ActionResult<List<EntryFtsDto>>> HandleAsync(
    public override Task<ActionResult> HandleAsync(
        [FromQuery] ListRequest listRequest,
        CancellationToken cancellationToken = new())
    {
        // var query = _entryRepository.FindByListRequest(listRequest);
        // var query = _entryRepository.FtsSearch(listRequest.Search ?? "");
        // return Task.FromResult<ActionResult>(Ok(query.ToList()));
        return Task.FromResult<ActionResult>(Ok());
    }
}
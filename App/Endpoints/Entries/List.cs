using App.DbConfigurations;
using App.Models;
using App.Repository;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace App.Endpoints.Entries;

public class List : EndpointBaseAsync
    .WithRequest<ListRequest>
    .WithActionResult<List<Entry>>
{
    private readonly EntryRepository _entryRepository;
    private readonly AppDbContext _appDbContext;

    public List(EntryRepository entryRepository, AppDbContext appDbContext)
    {
        _entryRepository = entryRepository;
        _appDbContext = appDbContext;
    }

    [HttpGet("/api/entries")]
    [SwaggerOperation(OperationId = "Entry.List", Tags = new[] {"Entry"})]
    public override async Task<ActionResult<List<Entry>>> HandleAsync(
        [FromQuery] ListRequest listRequest,
        CancellationToken cancellationToken = new())
    {
        var entries = await _entryRepository.PaginateListAsync(listRequest, cancellationToken);
        // var entries = await _appDbContext.Entries
        //         .Where(x => x.EntryType == listRequest.EntryType)
        //         .Take(49)
        //         .Skip(0)
        //         .ToListAsync(cancellationToken)
        //     ;
        return Ok(entries);
    }
}
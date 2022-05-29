using System.Text.Json.Nodes;
using App.Mappers;
using App.Models;
using App.Repository;
using App.Utils;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Annotations;

namespace App.Endpoints.Entries.EntryInfos;

public class Create : EndpointBaseAsync
    .WithRequest<CreateRequest>
    .WithActionResult
{
    private readonly EntryRepository _entryRepository;
    private readonly EntryInfoRepository _entryInfoRepository;
    private readonly IOptions<ApiBehaviorOptions> _apiOptions;

    public Create(
        EntryRepository entryRepository,
        EntryInfoRepository entryInfoRepository,
        IOptions<ApiBehaviorOptions> apiOptions
    )
    {
        _entryRepository = entryRepository;
        _entryInfoRepository = entryInfoRepository;
        _apiOptions = apiOptions;
    }

    [HttpPost("/api/entries/{entryId}/entry-infos")]
    [SwaggerOperation(OperationId = "EntryInfo.Create", Tags = new[] {"EntryInfo"})]
    public override async Task<ActionResult> HandleAsync(
        [FromMultiSource] CreateRequest request,
        CancellationToken cancellationToken = new()
    )
    {
        var validation = await new RequestDetailsValidator().ValidateAsync(request.Details, cancellationToken);
        if (!validation.IsValid)
        {
            validation.Errors.ForEach(e => { ModelState.AddModelError(e.PropertyName, e.ErrorMessage); });
            return (ActionResult) _apiOptions.Value.InvalidModelStateResponseFactory(ControllerContext);
        }

        var entry = await _entryRepository.FindByIdAsync(request.EntryId, cancellationToken);
        if (entry == null)
        {
            return NotFound("entry not found");
        }

        var eInfo = new EntryInfo {
            EntryId = entry.Id, 
            CreatedAt = DateTime.UtcNow,
            Type = request.Details.Type
        };

        request.Details.MapTo(eInfo);
        
        await _entryInfoRepository.CreateAsync(eInfo, cancellationToken);
        
        return CreatedAtRoute("EntryInfo_Get", new {entryId = entry.Id, entryInfoId = eInfo.Id}, eInfo);
    }
}
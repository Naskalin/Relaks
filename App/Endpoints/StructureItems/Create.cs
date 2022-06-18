using App.Mappers;
using App.Models;
using App.Repository;
using App.Utils;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Annotations;

namespace App.Endpoints.StructureItems;

public class Create : EndpointBaseAsync
    .WithRequest<CreateRequest>
    .WithActionResult
{
    private readonly StructureItemRepository _structureItemRepository;
    private readonly StructureItemDbValidate _structureItemDbValidate;
    private readonly IOptions<ApiBehaviorOptions> _apiOptions;

    public Create(
        StructureItemRepository structureItemRepository,
        StructureItemDbValidate structureItemDbValidate,
        IOptions<ApiBehaviorOptions> apiOptions
        )
    {
        _structureItemRepository = structureItemRepository;
        _structureItemDbValidate = structureItemDbValidate;
        _apiOptions = apiOptions;
    }

    [HttpPost("/api/structure-items")]
    [SwaggerOperation(OperationId = "StructureItem.Create", Tags = new[] {"StructureItem"})]
    public override async Task<ActionResult> HandleAsync(
        [FromMultiSource] CreateRequest request,
        CancellationToken cancellationToken = new()
    )
    {
        var validation = await new DetailsValidator().ValidateAsync(request.Details, cancellationToken);
        var errors = await _structureItemDbValidate.ValidateAsync(request.Details, cancellationToken);
        if (errors.Any()) errors.ForEach(e => {validation.Errors.Add(e);});
        if (!validation.IsValid)
        {
            validation.Errors.ForEach(e => { ModelState.AddModelError(e.PropertyName, e.ErrorMessage); });
            return (ActionResult) _apiOptions.Value.InvalidModelStateResponseFactory(ControllerContext);
        }

        var structureItem = new StructureItem()
        {
            CreatedAt = DateTime.UtcNow,
        };
        request.Details.MapTo(structureItem);
        await _structureItemRepository.CreateAsync(structureItem, cancellationToken);

        return CreatedAtRoute("StructureItem_Get", new {structureItemId = structureItem.Id}, structureItem);
    }
}
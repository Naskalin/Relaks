using App.Mappers;
using App.Repository;
using App.Utils;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Annotations;

namespace App.Endpoints.StructureItems;

public class Put : EndpointBaseAsync
    .WithRequest<PutRequest>
    .WithActionResult
{
    private readonly StructureItemDbValidate _structureItemDbValidate;
    private readonly IOptions<ApiBehaviorOptions> _apiOptions;
    private readonly StructureItemRepository _structureItemRepository;

    public Put(StructureItemDbValidate structureItemDbValidate, IOptions<ApiBehaviorOptions> apiOptions, StructureItemRepository structureItemRepository)
    {
        _structureItemDbValidate = structureItemDbValidate;
        _apiOptions = apiOptions;
        _structureItemRepository = structureItemRepository;
    }

    [HttpPut("/api/structure-items/{structureItemId}")]
    [SwaggerOperation(OperationId = "StructureItem.Put", Tags = new[] {"StructureItem"})]
    public override async Task<ActionResult> HandleAsync(
        [FromMultiSource] PutRequest request,
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

        var structureItem = await _structureItemRepository.FindByIdAsync(request.StructureItemId, cancellationToken);
        if (structureItem == null) return NotFound();
        request.Details.MapTo(structureItem);
        await _structureItemRepository.UpdateAsync(structureItem, cancellationToken);

        return NoContent();
    }
}
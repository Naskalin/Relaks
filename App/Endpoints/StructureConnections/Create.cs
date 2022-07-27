// using App.Mappers;
// using App.Models;
// using App.Repository;
// using App.Utils;
// using Ardalis.ApiEndpoints;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.Extensions.Options;
// using Swashbuckle.AspNetCore.Annotations;
//
// namespace App.Endpoints.StructureConnections;
//
// public class Create : EndpointBaseAsync
//     .WithRequest<CreateRequest>
//     .WithActionResult
// {
//     private readonly IOptions<ApiBehaviorOptions> _apiOptions;
//     private readonly StructureConnectionRepository _structureConnectionRepository;
//
//     public Create(IOptions<ApiBehaviorOptions> apiOptions, StructureConnectionRepository structureConnectionRepository)
//     {
//         _apiOptions = apiOptions;
//         _structureConnectionRepository = structureConnectionRepository;
//     }
//
//     [HttpPost("/api/structure-connections")]
//     [SwaggerOperation(OperationId = "StructureConnection.Create", Tags = new[] {"StructureConnection"})]
//     public override async Task<ActionResult> HandleAsync(
//         [FromMultiSource] CreateRequest request,
//         CancellationToken cancellationToken = new()
//     )
//     {
//         var validation = await new DetailsValidator().ValidateAsync(request.Details, cancellationToken);
//         if (!validation.IsValid)
//         {
//             validation.Errors.ForEach(e => { ModelState.AddModelError(e.PropertyName, e.ErrorMessage); });
//             return (ActionResult) _apiOptions.Value.InvalidModelStateResponseFactory(ControllerContext);
//         }
//
//         var structureConnection = new StructureConnection() {CreatedAt = DateTime.UtcNow};
//         request.Details.MapTo(structureConnection);
//         await _structureConnectionRepository.CreateAsync(structureConnection, cancellationToken);
//
//         return CreatedAtRoute(
//             "StructureConnection_Get",
//             new {structureConnectionId = structureConnection.Id},
//             structureConnection
//         );
//     }
// }
using System.Text.Json.Nodes;
using App.DbConfigurations;
using App.Utils;
using App.Utils.App;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Annotations;

namespace App.Endpoints.AppDataDir;

public class Create : EndpointBaseAsync
    .WithRequest<CreateRequest>
    .WithActionResult
{
    private readonly IOptions<ApiBehaviorOptions> _apiOptions;
    private readonly string _projectDir;

    public Create(IConfiguration configuration, IOptions<ApiBehaviorOptions> apiOptions)
    {
        _apiOptions = apiOptions;
        _projectDir = configuration.GetValue<string>(WebHostDefaults.ContentRootKey);;
    }

    [HttpPost("/api/app-data-dir")]
    [SwaggerOperation(OperationId = "AppDataDir.Create", Tags = new[] {"AppDataDir"})]
    public override async Task<ActionResult> HandleAsync(
        [FromMultiSource] CreateRequest request,
        CancellationToken cancellationToken = new()
    )
    {
        var validation = await new FormDetailsValidator().ValidateAsync(request.Details, cancellationToken);
        if (!validation.IsValid)
        {
            validation.Errors.ForEach(e => { ModelState.AddModelError(e.PropertyName, e.ErrorMessage); });
            return (ActionResult) _apiOptions.Value.InvalidModelStateResponseFactory(ControllerContext);
        }
        
        AppDataDirManager.UpdateConfigDir(_projectDir, request.Details.DataDir);
        var dataDir = AppDataDirManager.GetDirPath(_projectDir);
        if (dataDir == null) return BadRequest();
        
        return Ok(dataDir);
    }
}
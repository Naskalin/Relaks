using App.Utils.App;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace App.Endpoints.AppPreset;

public class Get : EndpointBaseAsync
    .WithoutRequest
    .WithActionResult
{
    private readonly string _projectDir;

    public Get(IConfiguration configuration)
    {
        _projectDir = configuration.GetValue<string>(WebHostDefaults.ContentRootKey);
    }
    
    [HttpGet("/api/app-preset")]
    [SwaggerOperation(OperationId = "AppPreset.Get", Tags = new[] {"AppPreset"})]
    public override Task<ActionResult> HandleAsync(
        CancellationToken cancellationToken = new()
    )
    {
        var appPreset = AppPresetManager.GetPreset(_projectDir);
        if (appPreset == null) return Task.FromResult<ActionResult>(NoContent());
        return Task.FromResult<ActionResult>(Ok(appPreset));
    }
}
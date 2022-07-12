using App.Utils.AppPreset;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace App.Endpoints.AppPreset;

public class Get : EndpointBaseAsync
    .WithoutRequest
    .WithActionResult<AppPresetPublic>
{
    private readonly string _projectDir;

    public Get(IConfiguration configuration)
    {
        _projectDir = configuration.GetValue<string>(WebHostDefaults.ContentRootKey);;
    }
    
    [HttpGet("/api/app-preset")]
    [SwaggerOperation(OperationId = "AppPreset.Get", Tags = new[] {"AppPreset"})]
    public override Task<ActionResult<AppPresetPublic>> HandleAsync(
        CancellationToken cancellationToken = new()
    )
    {
        var dataDir = AppDataDirManager.GetDataDir(_projectDir);
        if (dataDir == null) return Task.FromResult<ActionResult<AppPresetPublic>>(NoContent());
        var appPreset = AppPresetManager.GetPreset(dataDir.DirPath);
        return Task.FromResult<ActionResult<AppPresetPublic>>(Ok(appPreset));
    }
}
using App.Utils.App;
using Microsoft.AspNetCore.Authorization;

namespace App.Endpoints.AppPreset;

[HttpGet("/api/app-preset"), AllowAnonymous]
public class Get : EndpointWithoutRequest<AppPresetPublic>
{
    public override async Task HandleAsync(CancellationToken ct)
    {
        var projectDir = Config.GetValue<string>(WebHostDefaults.ContentRootKey);
        var appPreset = (AppPresetPublic?) AppPresetManager.GetPreset(projectDir);
        if (appPreset == null) await SendNoContentAsync(ct);
        else await SendAsync(appPreset, cancellation: ct);
    }
}
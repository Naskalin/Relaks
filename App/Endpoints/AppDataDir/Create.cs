using App.Utils.App;
using Microsoft.AspNetCore.Authorization;

namespace App.Endpoints.AppDataDir;

[HttpPost("/api/app-data-dir"), AllowAnonymous]
public class Create : Endpoint<CreateRequest>
{
    
    public override async Task HandleAsync(CreateRequest req, CancellationToken ct)
    {
        var projectDir = Config.GetValue<string>(WebHostDefaults.ContentRootKey);
        AppDataDirManager.UpdateConfigDir(projectDir, req.DataDir);
        var dataDir = AppDataDirManager.GetDirPath(projectDir);
        if (dataDir == null) ThrowError("dataDir is null");
        else await SendAsync(dataDir, cancellation: ct);
    }
}
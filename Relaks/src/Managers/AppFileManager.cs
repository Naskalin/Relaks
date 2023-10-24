using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Logging;
using Relaks.Interfaces;
using Relaks.Models;

namespace Relaks.Managers;

public class AppFileManager
{
    private readonly RelaksConfig _relaksConfig;
    private readonly ILogger<AppFileManager> _logger;

    public AppFileManager(RelaksConfig relaksConfig, ILogger<AppFileManager> logger)
    {
        _relaksConfig = relaksConfig;
        _logger = logger;
    }

    public string ToFullPath(IAppFile appFile)
    {
        return Path.Combine(_relaksConfig.FilesDirPath(), appFile.FilePath());
    }
    
    public async Task UploadAsync(BaseFile appFile, IBrowserFile file)
    {
        try
        {
            var fullPath = ToFullPath(appFile);
            
            // Create Directory if not exists
            var dirPath = Path.GetDirectoryName(fullPath);
            ArgumentNullException.ThrowIfNull(dirPath);
            if (!Directory.Exists(dirPath)) Directory.CreateDirectory(dirPath);
            
            // // Copy files to directory
            await using var stream = File.Create(fullPath);
            await file.OpenReadStream(long.MaxValue).CopyToAsync(stream);
        }
        catch (Exception ex)
        {
            _logger.LogError("File: {Id} - {Filename} Error: {Error}", 
                appFile.Id, appFile.Filename, ex.Message);
        }
    }

    public void DeleteIfExists(BaseFile appFile)
    {
        var filePathFull = ToFullPath(appFile);
        if (File.Exists(filePathFull)) File.Delete(filePathFull);
    }
}
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Relaks.Interfaces;
using Relaks.Models;

namespace Relaks.Managers;

public class AppFileManager
{
    private readonly RelaksConfigModel _relaksConfig;
    private readonly ILogger<AppFileManager> _logger;
    private readonly IWebHostEnvironment _env;

    public AppFileManager(RelaksConfigModel relaksConfig, ILogger<AppFileManager> logger, IWebHostEnvironment env)
    {
        _relaksConfig = relaksConfig;
        _logger = logger;
        _env = env;
    }

    private string ToFullPath(IAppFile appFile)
    {
        return Path.Combine(_relaksConfig.FilesDirPath, appFile.FilePath());
        // return Path.Combine(_env.WebRootPath, appFile.FilePath());
    }

    // public async Task UploadAsync(BaseFile appFile, IFormFile formFile)
    // {
    //     var filePathFull = ToFullPath(appFile);
    //     
    //     // Create Directory if not exists
    //     var dirPath = Path.GetDirectoryName(filePathFull);
    //     ArgumentNullException.ThrowIfNull(dirPath);
    //     if (!Directory.Exists(dirPath)) Directory.CreateDirectory(dirPath);
    //
    //     // Copy files to directory
    //     await using var stream = File.Create(filePathFull);
    //     await formFile.CopyToAsync(stream);
    // }
    
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

    public void RemoveIfExists(BaseFile appFile)
    {
        var filePathFull = ToFullPath(appFile);
        if (File.Exists(filePathFull)) File.Delete(filePathFull);
    }
}
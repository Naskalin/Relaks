using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Relaks.Interfaces;
using Relaks.Models;

namespace Relaks.Managers;

public class AppFileManager
{
    private readonly IWebHostEnvironment _env;

    public AppFileManager(IWebHostEnvironment env)
    {
        _env = env;
    }

    private string ToFullPath(IAppFile appFile)
    {
        return Path.Combine(_env.WebRootPath, appFile.FilePath());
    }

    public async Task UploadAsync(BaseFile appFile, IFormFile formFile)
    {
        var filePathFull = ToFullPath(appFile);
        
        // Create Directory if not exists
        var dirPath = Path.GetDirectoryName(filePathFull);
        ArgumentNullException.ThrowIfNull(dirPath);
        if (!Directory.Exists(dirPath)) Directory.CreateDirectory(dirPath);

        // Copy files to directory
        await using var stream = File.Create(filePathFull);
        await formFile.CopyToAsync(stream);
    }

    public void RemoveIfExists(BaseFile appFile)
    {
        var filePathFull = ToFullPath(appFile);
        if (File.Exists(filePathFull)) File.Delete(filePathFull);
    }
}
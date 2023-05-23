using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using MimeTypes;
using Relaks.Models;

namespace Relaks.Mappers;

public static class AppFileMapper
{
    private static string GetExtension(string contentType, string fileName)
    {
        string extension;

        try
        {
            extension = MimeTypeMap.GetExtension(contentType);
        }
        catch
        {
            extension = Path.GetExtension(fileName);
        }

        return extension;
    }

    // public static void MapTo(this IFormFile formFile, BaseFile appFile)
    // {
    //     appFile.Filename = Path.GetRandomFileName() + GetExtension(formFile.ContentType, formFile.FileName);
    //     appFile.MimeType = formFile.ContentType;
    //     appFile.DisplayName = Path.GetFileNameWithoutExtension(formFile.FileName);
    // }

    public static void MapTo(this IBrowserFile browserFile, BaseFile appFile)
    {
        var extension = GetExtension(browserFile.ContentType, browserFile.Name);
        appFile.Filename = Path.GetRandomFileName() + extension;
        appFile.MimeType = browserFile.ContentType;
        appFile.DisplayName = Path.GetFileNameWithoutExtension(browserFile.Name);
    }
}
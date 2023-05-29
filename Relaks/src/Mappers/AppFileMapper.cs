using Microsoft.AspNetCore.Components.Forms;
using MimeTypes;
using Relaks.Interfaces;
using Relaks.Models;

namespace Relaks.Mappers;

public static class AppFileMapper
{
    public static string ToFtsBody(this BaseFile baseFile)
    {
        var arr = new List<string?>
        {
            baseFile.DisplayNameWithExtension(),
            baseFile.DeletedReason
        };

        return string.Join(" ", arr.Where(x => !string.IsNullOrEmpty(x)));
    }
    
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
        
        var mimeType = browserFile.ContentType;
        if (string.IsNullOrEmpty(mimeType))
        {
            mimeType = MimeTypeMap.GetMimeType(extension);
        }
        
        appFile.Filename = Path.GetRandomFileName() + extension;
        appFile.MimeType = mimeType;
        appFile.DisplayName = Path.GetFileNameWithoutExtension(browserFile.Name);
    }

    public static void MapTo(this IAppFile from, IAppFile to)
    {
        to.DisplayName = from.DisplayName.Trim();
    }
}
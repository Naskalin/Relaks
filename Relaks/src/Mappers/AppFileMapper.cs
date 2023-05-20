using Microsoft.AspNetCore.Http;
using MimeTypes;
using Relaks.Models;

namespace Relaks.Mappers;

public static class AppFileMapper
{
    /// <summary>
    /// Получаем расширение файла, исходя из его типа, а не названия
    /// </summary>
    /// <param name="formFile"></param>
    /// <returns></returns>
    private static string GetExtension(IFormFile formFile)
    {
        string extension;
        
        try
        {
            extension = MimeTypeMap.GetExtension(formFile.ContentType);
        }
        catch
        {
            extension = Path.GetExtension(formFile.FileName);
        }
        
        return extension;
    }

    public static void MapTo(this IFormFile formFile, BaseFile appFile)
    {
        appFile.Extension = GetExtension(formFile);
        appFile.MimeType = formFile.ContentType;
        appFile.DisplayName = Path.GetFileNameWithoutExtension(formFile.FileName);
    }
}
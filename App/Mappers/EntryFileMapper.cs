using App.Endpoints.Entries.EntryFiles;
using App.Models;

namespace App.Mappers;

public static class EntryFileMapper
{
    public static void MapToCreate(this IFormFile formFile, FileModel model)
    {
        var guid = Guid.NewGuid();
        model.Id = guid;
        model.Path = guid + Path.GetExtension(formFile.FileName);
        
        model.ContentType = formFile.ContentType;
        model.Name = Path.GetFileNameWithoutExtension(formFile.FileName);
        model.CreatedAt = DateTime.UtcNow;
        model.UpdatedAt = DateTime.UtcNow;
        model.DeletedReason = "";
    }

    public static void MapTo(this EntryFilePutDetails req, EntryFile model)
    {
        model.Name = req.Name.Trim();
        model.UpdatedAt = DateTime.UtcNow;
        req.SoftDeleteMapTo(model);
    }
}
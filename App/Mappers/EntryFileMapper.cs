using App.Endpoints.EntryFiles;
using App.Models;

namespace App.Mappers;

public static class EntryFileMapper
{
    public static void MapToCreate(this IFormFile formFile, FileModel model)
    {
        var guid = Guid.NewGuid();
        model.Id = guid;
        model.Path = guid + Path.GetExtension(formFile.FileName);
        // model.Category = "";
        // model.TempCategory = "";
        model.ContentType = formFile.ContentType;
        model.Name = Path.GetFileNameWithoutExtension(formFile.FileName);
        model.CreatedAt = DateTime.UtcNow;
        model.UpdatedAt = DateTime.UtcNow;
        model.DeletedReason = "";
    }

    public static void MapTo(this EntryFilePutRequest req, EntryFile model)
    {
        model.Name = req.Name.Trim();
        model.UpdatedAt = DateTime.UtcNow;
        // model.Category = "";
        // model.Category = req.Category.Trim();
        // model.TempCategory = "";
        model.Tags = req.Tags;
        req.SoftDeleteMapTo(model);
    }
}
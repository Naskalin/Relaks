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
        model.Name = formFile.FileName;
        model.CreatedAt = DateTime.UtcNow;
        model.UpdatedAt = DateTime.UtcNow;
        model.DeletedReason = "";
    }
}
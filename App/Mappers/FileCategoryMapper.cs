using App.Endpoints.FileCategories;
using App.Models;

namespace App.Mappers;

public static class FileCategoryMapper
{
    public static void MapTo(this FileCategoryFormRequest req, FileCategory fileCategory)
    {
        fileCategory.Title = req.Title.Trim();
        fileCategory.UpdatedAt = DateTime.UtcNow;
        fileCategory.ParentId = req.ParentId;
        
        req.MapToSoftDelete(fileCategory);
    }
}
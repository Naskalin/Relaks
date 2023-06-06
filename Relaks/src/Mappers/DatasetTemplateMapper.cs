using Relaks.Models;

namespace Relaks.Mappers;

public static class DatasetTemplateMapper
{
    public static void MapTo(this DatasetTemplate model, DatasetTemplateRequest req)
    {
        req.Title = model.Title;
        req.Dataset = model.Dataset;
    }
    
    public static void MapTo(this DatasetTemplateRequest req, DatasetTemplate model)
    {
        model.Title = req.Title.Trim();
        model.Dataset = req.Dataset;
    }
}
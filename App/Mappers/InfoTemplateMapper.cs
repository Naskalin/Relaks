using App.Endpoints.InfoTemplates;
using App.Models;

namespace App.Mappers;

public static class InfoTemplateMapper
{
    public static void MapTo(this FormRequestDetails details, InfoTemplate infoTemplate)
    {
        infoTemplate.UpdatedAt = DateTime.UtcNow;
        infoTemplate.Title = details.Title.Trim();
        infoTemplate.Template = details.Template;
    }
}
using App.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace App.Endpoints.InfoTemplates;

[HttpGet("/api/info-templates"), AllowAnonymous]
public class List : Endpoint<ListRequest>
{
    private readonly InfoTemplateRepository _infoTemplateRepository;

    public List(InfoTemplateRepository infoTemplateRepository)
    {
        _infoTemplateRepository = infoTemplateRepository;
    }
    
    public override async Task HandleAsync(ListRequest listRequest, CancellationToken ct)
    {
        var templates = await _infoTemplateRepository
            .FindByListRequest(listRequest)
            .ToListAsync(ct);

        await SendOkAsync(templates, ct);
    }
}
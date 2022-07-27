using App.Repository;
using Microsoft.AspNetCore.Authorization;

namespace App.Endpoints.InfoTemplates;

[HttpGet("/api/info-templates/{infoTemplateId:guid}"), AllowAnonymous]
public class Get : Endpoint<GetRequest>
{
    private readonly InfoTemplateRepository _infoTemplateRepository;

    public Get(InfoTemplateRepository infoTemplateRepository)
    {
        _infoTemplateRepository = infoTemplateRepository;
    }
    
    public override async Task HandleAsync(GetRequest req, CancellationToken ct)
    {
        var infoTemplate = await _infoTemplateRepository.FindByIdAsync(req.InfoTemplateId, ct);
        if (infoTemplate == null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        await SendOkAsync(infoTemplate, ct);
    }
}
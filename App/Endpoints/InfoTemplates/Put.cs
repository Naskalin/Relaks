using App.Mappers;
using App.Repository;
using Microsoft.AspNetCore.Authorization;

namespace App.Endpoints.InfoTemplates;

[HttpPut("/api/info-templates/{infoTemplateId}"), AllowAnonymous]
public class Put : Endpoint<PutRequest>
{
    private readonly InfoTemplateRepository _infoTemplateRepository;

    public Put(InfoTemplateRepository infoTemplateRepository)
    {
        _infoTemplateRepository = infoTemplateRepository;
    }

    public override async Task HandleAsync(PutRequest request, CancellationToken ct)
    {
        var infoTemplate = await _infoTemplateRepository.FindByIdAsync(request.InfoTemplateId, ct);
        if (infoTemplate == null)
        {
            await SendNotFoundAsync(ct);
            return;
        }
        
        request.MapTo(infoTemplate);
        await _infoTemplateRepository.UpdateAsync(infoTemplate, ct);
        await SendNoContentAsync(ct);
    }
}
using App.Repository;
using Microsoft.AspNetCore.Authorization;

namespace App.Endpoints.InfoTemplates;

[HttpDelete("/api/info-templates/{infoTemplateId:guid}"), AllowAnonymous]
public class Delete : Endpoint<GetRequest>
{
    private readonly InfoTemplateRepository _infoTemplateRepository;

    public Delete(InfoTemplateRepository infoTemplateRepository)
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

        await _infoTemplateRepository.DeleteAsync(infoTemplate, ct);
        await SendNoContentAsync(ct);
    }
}
using App.Mappers;
using App.Models;
using App.Repository;
using Microsoft.AspNetCore.Authorization;

namespace App.Endpoints.InfoTemplates;

[HttpPost("/api/info-templates"), AllowAnonymous]
public class Create : Endpoint<CreateRequest>
{
    private readonly InfoTemplateRepository _infoTemplateRepository;

    public Create(InfoTemplateRepository infoTemplateRepository)
    {
        _infoTemplateRepository = infoTemplateRepository;
    }

    public override async Task HandleAsync(CreateRequest req, CancellationToken ct)
    {
        var infoTemplate = new InfoTemplate {CreatedAt = DateTime.UtcNow};
        req.MapTo(infoTemplate);
        
        await _infoTemplateRepository.CreateAsync(infoTemplate, ct);
        await SendCreatedAtAsync<Get>(new {infoTemplateId = infoTemplate.Id}, infoTemplate, cancellation: ct);
    }
}
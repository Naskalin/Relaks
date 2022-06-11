using App.Models;
using App.Repository;
using App.Utils;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace App.Endpoints.Entries.EntryFiles.Meta;

public class Put : EndpointBaseAsync
    .WithRequest<PutRequest>
    .WithActionResult
{
    private readonly EntryFileRepository _entryFileRepository;

    public Put(EntryFileRepository entryFileRepository)
    {
        _entryFileRepository = entryFileRepository;
    }

    [HttpPut("/api/entries/{entryId}/files/meta")]
    [SwaggerOperation(OperationId = "EntryFileMeta.Put", Tags = new[] {"EntryFileMeta"})]
    public override async Task<ActionResult> HandleAsync(
        [FromMultiSource] PutRequest request,
        CancellationToken cancellationToken = new()
    )
    {
        // validation
        var query = _entryFileRepository.Entities.Where(x => x.EntryId == request.EntryId);
        List<EntryFile> entryFiles = new();
        if (request.Details.Field == FileMetaFieldsEnum.Category)
        {
            entryFiles = await query
                .Where(x => x.Category == request.Details.Value)
                .ToListAsync(cancellationToken);
        }
        else
        {
            entryFiles = query
                    .AsEnumerable()
                    .Where(x => x.Tags.Any(tag => tag == request.Details.Value))
                    .ToList()
                ;
        }

        if (!entryFiles.Any()) return NotFound();

        if (request.Details.Field == FileMetaFieldsEnum.Category)
        {
            foreach (var entryFile in entryFiles)
            {
                entryFile.Category = request.Details.NewValue;
            }
        }
        else
        {
            foreach (var entryFile in entryFiles)
            {
                var i = 0;
                foreach (var tag in entryFile.Tags)
                {
                    if (tag == request.Details.Value)
                    {
                        entryFile.Tags[i] = request.Details.NewValue;
                        break;
                    };
                    i++;
                }
            }
        }

        await _entryFileRepository.UpdateMultipleAsync(entryFiles, cancellationToken);

        return NoContent();
    }
}
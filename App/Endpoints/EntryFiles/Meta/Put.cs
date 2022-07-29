using App.Models;
using App.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace App.Endpoints.EntryFiles.Meta;

[HttpPut("/api/entries/{entryId:guid}/files/meta"), AllowAnonymous]
public class Put : Endpoint<PutMetaRequest>
{
    private readonly EntryFileRepository _entryFileRepository;

    public Put(EntryFileRepository entryFileRepository)
    {
        _entryFileRepository = entryFileRepository;
    }
    
    public override async Task HandleAsync(PutMetaRequest req, CancellationToken ct)
    {
        var query = _entryFileRepository.Entities.Where(x => x.EntryId == req.EntryId);
        List<EntryFile> entryFiles;
        // if (req.Field == FileMetaFieldsEnum.Category)
        // {
            // entryFiles = await query
            //     .Where(x => x.Category == req.Value)
            //     .ToListAsync(ct);
        // }
        // else
        // {
            entryFiles = query
                    .AsEnumerable()
                    .Where(x => x.Tags.Any(tag => tag == req.Value))
                    .ToList()
                ;
        // }

        if (!entryFiles.Any())
        {
            await SendNotFoundAsync(ct);
            return;
        }

        // if (req.Field == FileMetaFieldsEnum.Category)
        // {
        //     foreach (var entryFile in entryFiles)
        //     {
        //         entryFile.Category = req.NewValue;
        //     }
        // }
        // else
        // {
            foreach (var entryFile in entryFiles)
            {
                var i = 0;
                foreach (var tag in entryFile.Tags)
                {
                    if (tag == req.Value)
                    {
                        entryFile.Tags[i] = req.NewValue;
                        break;
                    };
                    i++;
                }
            }
        // }

        await _entryFileRepository.UpdateMultipleAsync(entryFiles, ct);
        await SendNoContentAsync(ct);
    }
}
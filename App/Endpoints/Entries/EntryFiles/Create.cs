﻿using App.Mappers;
using App.Models;
using App.Repository;
using App.Utils;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;

namespace App.Endpoints.Entries.EntryFiles;

public class Create : EndpointBaseAsync
    .WithRequest<CreateRequest>
    .WithActionResult

{
    private readonly EntryFileRepository _entryFileRepository;
    private readonly EntryRepository _entryRepository;

    public Create(EntryFileRepository entryFileRepository, EntryRepository entryRepository)
    {
        _entryFileRepository = entryFileRepository;
        _entryRepository = entryRepository;
    }

    [HttpPost("/api/entries/{entryId}/files")]
    public override async Task<ActionResult> HandleAsync(
        [FromMultiSource] CreateRequest request,
        CancellationToken cancellationToken = new()
    )
    {
        var entry = await _entryRepository.FindByIdAsync(request.EntryId, cancellationToken);
        if (entry == null)
        {
            return NotFound();
        }

        var size = request.Files.Sum(f => f.Length);
        foreach (var formFile in request.Files)
        {
            if (formFile.Length > 0)
            {
                var entryFile = new EntryFile();
                entryFile.EntryId = entry.Id;
                formFile.MapToCreate(entryFile);

                var folder = Path.Combine(
                    "C:\\app\\RiderProjects\\Relaks\\App\\Data\\files",
                    entryFile.EntryId.ToString()
                );
                if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);
                var filePath = Path.Combine(folder, entryFile.Path);

                using (var stream = System.IO.File.Create(filePath))
                {
                    await formFile.CopyToAsync(stream, cancellationToken);
                }

                await _entryFileRepository.CreateAsync(entryFile, cancellationToken);
                // await _entryFileRepository.CreateRangeAsync(entryFiles, cancellationToken);
            }
        }

        return Ok(new {count = request.Files.Count, size});
    }
}
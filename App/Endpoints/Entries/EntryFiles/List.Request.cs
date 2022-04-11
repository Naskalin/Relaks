using Microsoft.AspNetCore.Mvc;

namespace App.Endpoints.Entries.EntryFiles;

public class ListRequest
{
    [FromRoute]
    public Guid EntryId { get; set; }   
}
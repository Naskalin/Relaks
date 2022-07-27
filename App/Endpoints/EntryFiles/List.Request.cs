using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;

namespace App.Endpoints.Entries.EntryFiles;

public class ListRequest : BaseListDeletableRequest
{
    [FromRoute]
    public Guid EntryId { get; set; }

    [FromQuery]
    [DefaultValue("")]
    public string? Category { get; set; } = "";

    [FromQuery]
    public List<string> Tags { get; set; } = new();
}
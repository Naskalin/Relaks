using System.Text.Json.Nodes;
using App.Models;
using App.Utils;

namespace App.Endpoints.Entries.EntryInfos;

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

public class CreateRequest
{
    [FromRoute]
    [Required]
    public Guid EntryId { get; set; }

    [FromBody]
    public EntryInfoRequestDetails Details { get; set; } = null!;
}

public class EntryInfoRequestDetails : IEntryInfoFormCommonRequest, IInfoData
{
    public string Title { get; set; } = null!;
    public DateTime? DeletedAt { get; set; }
    public string DeletedReason { get; set; } = null!;

    public string Type { get; set; } = null!;
    public JsonObject Info { get; set; } = null!;
}
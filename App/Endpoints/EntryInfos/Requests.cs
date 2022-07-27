using System.Text.Json.Nodes;
using App.Models;
using App.Utils;

namespace App.Endpoints.EntryInfos;

public class EntryInfoFormRequest : ISoftDelete, IInfoData
{
    public string Title { get; set; } = null!;
    public DateTime? DeletedAt { get; set; }
    public string DeletedReason { get; set; } = null!;
    public bool IsFavorite { get; set; }

    public string Type { get; set; } = null!;
    public JsonObject Info { get; set; } = null!;
}

public class EntryInfoCreateRequest : EntryInfoFormRequest
{
    public Guid EntryId { get; set; }
}

public class EntryInfoPutRequest : EntryInfoFormRequest
{
    public Guid EntryId { get; set; }
    public Guid EntryInfoId { get; set; }
}

public class EntryInfoGetRequest
{
    public Guid EntryId { get; set; }
    public Guid EntryInfoId { get; set; }
}

public class EntryInfoListRequest : BaseListRequest
{
    public Guid EntryId { get; set; }
    public List<string>? Type { get; set; }
}
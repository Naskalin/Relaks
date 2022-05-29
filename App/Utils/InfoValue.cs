using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using App.Models;

namespace App.Utils;

public interface IInfoData
{
    public EntryInfoType Type { get; }
    public JsonObject Data { get; }
}

public static class InfoValue
{
    public static readonly JsonSerializerOptions WriteOptions = new(JsonSerializerDefaults.Web)
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };

    public static InfoEmail? Email(this IInfoData eInfo)
    {
        if (eInfo.Type != EntryInfoType.Email) return null;
        return eInfo.Data.Deserialize<InfoEmail>(WriteOptions);
    }
}
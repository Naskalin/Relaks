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

    public static EmailInfo? Email(this IInfoData eInfo)
    {
        if (eInfo.Type != EntryInfoType.Email) return null;
        return eInfo.Data.Deserialize<EmailInfo>(WriteOptions);
    }
    
    public static PhoneInfo? PhoneUnformatted(this IInfoData eInfo)
    {
        if (eInfo.Type != EntryInfoType.Phone) return null;
        return eInfo.Data.Deserialize<PhoneInfo>(WriteOptions);
    }
    
    public static PhoneInfo? Phone(this IInfoData eInfo)
    {
        if (eInfo.Type != EntryInfoType.Phone) return null;
        var phone = eInfo.Data.Deserialize<PhoneInfo>(WriteOptions);
        return PhoneHelper.ToPhone(phone.Number, phone.Region);
    }
    
    public static NoteInfo? Note(this IInfoData eInfo)
    {
        if (eInfo.Type != EntryInfoType.Note) return null;
        return eInfo.Data.Deserialize<NoteInfo>(WriteOptions);
    }
    
    public static DateInfo? Date(this IInfoData eInfo)
    {
        if (eInfo.Type != EntryInfoType.Date) return null;
        return eInfo.Data.Deserialize<DateInfo>(WriteOptions);
    }
    
    public static UrlInfo? Url(this IInfoData eInfo)
    {
        if (eInfo.Type != EntryInfoType.Url) return null;
        return eInfo.Data.Deserialize<UrlInfo>(WriteOptions);
    }
}
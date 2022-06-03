﻿using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using App.Models;

namespace App.Utils;

public interface IInfoData
{
    public string Type { get; }
    public JsonObject Info { get; }
}

public static class InfoValue
{
    public static readonly JsonSerializerOptions WriteOptions = new(JsonSerializerDefaults.Web)
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };

    // public static EntryInfoType EntryInfoType(this IInfoData eInfoData) => new(eInfoData.Type);
        
    public static EmailInfo? Email(this IInfoData eInfo)
    {
        if (!InfoBaseType.Email.Equals(eInfo.Type)) return null;
        return eInfo.Info.Deserialize<EmailInfo>(WriteOptions);
    }
    
    public static PhoneInfo? PhoneUnformatted(this IInfoData eInfo)
    {
        if (!InfoBaseType.Phone.Equals(eInfo.Type)) return null;
        return eInfo.Info.Deserialize<PhoneInfo>(WriteOptions);
    }
    
    public static PhoneInfo? Phone(this IInfoData eInfo)
    {
        if (!InfoBaseType.Phone.Equals(eInfo.Type)) return null;
        var phone = eInfo.Info.Deserialize<PhoneInfo>(WriteOptions)!;
        return PhoneHelper.ToPhone(phone.Number, phone.Region);
    }
    
    public static NoteInfo? Note(this IInfoData eInfo)
    {
        if (!InfoBaseType.Note.Equals(eInfo.Type)) return null;
        return eInfo.Info.Deserialize<NoteInfo>(WriteOptions);
    }
    
    public static DateInfo? Date(this IInfoData eInfo)
    {
        if (!InfoBaseType.Date.Equals(eInfo.Type)) return null;
        return eInfo.Info.Deserialize<DateInfo>(WriteOptions);
    }
    
    public static UrlInfo? Url(this IInfoData eInfo)
    {
        if (!InfoBaseType.Url.Equals(eInfo.Type)) return null;
        return eInfo.Info.Deserialize<UrlInfo>(WriteOptions);
    }
}
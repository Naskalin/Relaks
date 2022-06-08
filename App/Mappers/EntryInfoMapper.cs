using System.Text.Json;
using App.Endpoints.Entries.EntryInfos;
using App.Models;
using App.Utils;

namespace App.Mappers;

public static class EntryInfoMapper
{
    public static void MapTo(this EntryInfoRequestDetails details, EntryInfo eInfo)
    {
        details.SoftDeleteMapTo(eInfo);
        eInfo.Title = details.Title.Trim();
        eInfo.UpdatedAt = DateTime.UtcNow;

        switch (eInfo.Type.ToUpper())
        {
            // case nameof(InfoBaseType.Email):
            case EntryInfo.Email:
                var email = details.Email()!;
                email.Email = email.Email.Trim().ToLower();
                eInfo.Value = JsonSerializer.Serialize(email, InfoValue.WriteOptions);
                break;
            case EntryInfo.Phone:
                eInfo.Value = JsonSerializer.Serialize(details.Phone()!, InfoValue.WriteOptions);
                break;
            case EntryInfo.Url:
                var url = details.Url()!;
                url.Url = url.Url.Trim();
                eInfo.Value = JsonSerializer.Serialize(url, InfoValue.WriteOptions);
                break;
            case EntryInfo.Note:
                eInfo.Value = JsonSerializer.Serialize(details.Note()!, InfoValue.WriteOptions);
                break;
            case EntryInfo.Date:
                eInfo.Value = JsonSerializer.Serialize(details.Date()!, InfoValue.WriteOptions);
                break;
            case EntryInfo.Custom:
                eInfo.Value = JsonSerializer.Serialize(details.Custom()!, InfoValue.WriteOptions);
                break;
            default:
                throw new ArgumentException($"EntryInfo Mapper for type: {eInfo.Type} not found.");
        }
    }
}
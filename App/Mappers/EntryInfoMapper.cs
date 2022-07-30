using System.Text.Json;
using App.Endpoints.EntryInfos;
using App.Models;
using App.Utils;

namespace App.Mappers;

public static class EntryInfoMapper
{
    public static void MapTo(this EntryInfoFormRequest formRequest, EntryInfo eInfo)
    {
        formRequest.MapToSoftDelete(eInfo);
        eInfo.Title = formRequest.Title.Trim();
        eInfo.IsFavorite = formRequest.IsFavorite;
        eInfo.UpdatedAt = DateTime.UtcNow;

        switch (eInfo.Type.ToUpper())
        {
            case EntryInfo.Email:
                var email = formRequest.Email()!;
                email.Email = email.Email.Trim().ToLower();
                eInfo.Value = JsonSerializer.Serialize(email, InfoValue.WriteOptions);
                break;
            case EntryInfo.Phone:
                eInfo.Value = JsonSerializer.Serialize(formRequest.Phone()!, InfoValue.WriteOptions);
                break;
            case EntryInfo.Url:
                var url = formRequest.Url()!;
                url.Url = url.Url.Trim();
                eInfo.Value = JsonSerializer.Serialize(url, InfoValue.WriteOptions);
                break;
            case EntryInfo.Note:
                eInfo.Value = JsonSerializer.Serialize(formRequest.Note()!, InfoValue.WriteOptions);
                break;
            case EntryInfo.Date:
                eInfo.Value = JsonSerializer.Serialize(formRequest.Date()!, InfoValue.WriteOptions);
                break;
            case EntryInfo.Custom:
                eInfo.Value = JsonSerializer.Serialize(formRequest.Custom()!, InfoValue.WriteOptions);
                break;
            default:
                throw new ArgumentException($"EntryInfo Mapper for type: {eInfo.Type} not found.");
        }
    }
}
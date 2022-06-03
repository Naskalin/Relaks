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

        if (InfoBaseType.Email.Equals(eInfo.Type))
        {
            var email = details.Email()!;
            email.Email = email.Email.Trim().ToLower();
            eInfo.Value = JsonSerializer.Serialize(email, InfoValue.WriteOptions);
        }
        else if (InfoBaseType.Phone.Equals(eInfo.Type))
        {
            eInfo.Value = JsonSerializer.Serialize(details.Phone()!, InfoValue.WriteOptions);
        }
        else if (InfoBaseType.Url.Equals(eInfo.Type))
        {
            var url = details.Url()!;
            url.Url = url.Url.Trim();
            eInfo.Value = JsonSerializer.Serialize(url, InfoValue.WriteOptions);
        }
        else if (InfoBaseType.Note.Equals(eInfo.Type))
        {
            eInfo.Value = JsonSerializer.Serialize(details.Note()!, InfoValue.WriteOptions);
        }
        else if (InfoBaseType.Date.Equals(eInfo.Type))
        {
            eInfo.Value = JsonSerializer.Serialize(details.Date()!, InfoValue.WriteOptions);
        }
        else
        {
            throw new ArgumentException($"EntryInfo Mapper for type: {eInfo.Type} not found.");
        }
    }
}
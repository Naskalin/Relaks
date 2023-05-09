using Relaks.Models;
using Relaks.Models.Requests.EntryInfoRequests;

namespace Relaks.Mappers;

public static class EntryInfoMapper
{
    public static void MapTo(this EntryInfoFormRequest req, EiPhone eiPhone)
    {
        ArgumentException.ThrowIfNullOrEmpty(req.Number);
        ArgumentException.ThrowIfNullOrEmpty(req.Region);
        
        eiPhone.Region = req.Region;
        eiPhone.Number = req.Number;
    }
    
    public static void MapTo(this EntryInfoFormRequest req, EiEmail eiEmail)
    {
        ArgumentException.ThrowIfNullOrEmpty(req.Email);
        eiEmail.Email = req.Email;
    }
    
    public static void MapTo(this EntryInfoFormRequest req, EiUrl eiUrl)
    {
        ArgumentException.ThrowIfNullOrEmpty(req.Url);
        eiUrl.Url = req.Url;
    }
    
    public static void MapTo(this EntryInfoFormRequest req, EiDate eiDate)
    {
        ArgumentNullException.ThrowIfNull(req.Date);
        ArgumentNullException.ThrowIfNull(req.WithTime);
        
        eiDate.Date = req.Date.Value;
        eiDate.WithTime = req.WithTime.Value;
    }

    public static void MapTo(this EntryInfoFormRequest req, BaseEntryInfo eInfo)
    {
        eInfo.Title = req.Title;
        eInfo.IsFavorite = req.IsFavorite;
    }
    
    public static string ToFtsBody(this BaseEntryInfo eInfo)
    {
        var arr = new List<string?>() {eInfo.Title};

        switch (eInfo.Discriminator)
        {
            case nameof(EiEmail):
                arr.Add(((EiEmail) eInfo).Email);
                break;
            case nameof(EiPhone):
                arr.Add(((EiPhone) eInfo).Number);
                break;
            case nameof(EiUrl):
                arr.Add(((EiUrl) eInfo).Url);
                break;
            case nameof(EiCustom):
                foreach (var group in ((EiCustom) eInfo).Custom.Groups)
                {
                    arr.Add(group.Title);
                    foreach (var item in group.Items)
                    {
                        arr.Add(item.Key);
                        arr.Add(item.Value);
                    }
                }
                break;
        }
        
        arr.Add(eInfo.DeletedReason);

        return String.Join(" ", arr.Where(x => !string.IsNullOrEmpty(x)));
    }
}
using Relaks.Models;

namespace Relaks.Mappers;

public static class EntryInfoMapper
{
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
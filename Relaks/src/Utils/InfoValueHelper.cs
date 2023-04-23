// using System.Text.Json;
// using System.Text.Json.Serialization;
// using Relaks.Interfaces;
// using Relaks.Models;
//
// namespace Relaks.Utils;
//
// public static class InfoValueHelper
// {
//     public static readonly JsonSerializerOptions WriteOptions = new(JsonSerializerDefaults.Web)
//     {
//         PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
//         DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
//     };
//
//     public static EmailInfo? Email(this IInfoData eInfo)
//     {
//         if (!EntryInfo.Email.Equals(eInfo.Type)) return null;
//         return eInfo.Info.Deserialize<EmailInfo>(WriteOptions);
//     }
//     
//     public static PhoneInfo? PhoneUnformatted(this IInfoData eInfo)
//     {
//         if (!EntryInfo.Phone.Equals(eInfo.Type)) return null;
//         return eInfo.Info.Deserialize<PhoneInfo>(WriteOptions);
//     }
//     
//     public static PhoneInfo? Phone(this IInfoData eInfo)
//     {
//         if (!EntryInfo.Phone.Equals(eInfo.Type)) return null;
//         var phone = eInfo.Info.Deserialize<PhoneInfo>(WriteOptions)!;
//         return PhoneHelper.ToPhone(phone.Number, phone.Region);
//     }
//     
//     public static NoteInfo? Note(this IInfoData eInfo)
//     {
//         if (!EntryInfo.Note.Equals(eInfo.Type)) return null;
//         return eInfo.Info.Deserialize<NoteInfo>(WriteOptions);
//     }
//     
//     public static DateInfo? Date(this IInfoData eInfo)
//     {
//         if (!EntryInfo.Date.Equals(eInfo.Type)) return null;
//         return eInfo.Info.Deserialize<DateInfo>(WriteOptions);
//     }
//     
//     public static CustomInfo? Custom(this IInfoData eInfo)
//     {
//         if (!EntryInfo.Custom.Equals(eInfo.Type)) return null;
//         return eInfo.Info.Deserialize<CustomInfo>(WriteOptions);
//     }
//     
//     public static UrlInfo? Url(this IInfoData eInfo)
//     {
//         if (!EntryInfo.Url.Equals(eInfo.Type)) return null;
//         return eInfo.Info.Deserialize<UrlInfo>(WriteOptions);
//     }
// }
using System.ComponentModel.DataAnnotations;
using JsonApiDotNetCore.Resources;
using JsonApiDotNetCore.Resources.Annotations;

namespace App.Models;

public abstract class EntryInfo : Identifiable<Guid>, ITimestampResource, IActualResource
{
    [Attr]
    public List<Entry> Entries { get; set; } = new();

    [Attr(Capabilities = AttrCapabilities.AllowView)]
    public DateTime CreatedAt { get; set; }

    [Attr(Capabilities = AttrCapabilities.AllowView)]
    public DateTime UpdatedAt { get; set; }

    [Attr]
    public DateTime ActualFrom { get; set; }

    [Attr]
    public DateTime? ActualTo { get; set; }

    [Attr]
    [MaxLength(200)]
    public string? ActualToReason { get; set; }

    [Attr]
    [MaxLength(200)]
    public string? ActualFromReason { get; set; }

    [Attr]
    [MaxLength(200)]
    public string? About { get; set; } = null!;
    
    [Attr(Capabilities = AttrCapabilities.AllowView)]
    public EntryInfoTypeEnum InfoType { get; set; }
}

public enum EntryInfoTypeEnum
{
    Date,
    Text
}

public class InfoDate : EntryInfo
{
    [Attr]
    public DateTime Date { get; set; }

    [Attr]
    [MaxLength(30)]
    public InfoDateTypeEnum? DateType { get; set; }
}

public enum InfoDateTypeEnum
{
    // PersonBirthday,
    // PersonDeath,
    FirstMeet,
    // MeetStart,
    // MeetEnd,
    // CompanyCreation,
    // CompanyClosing,
}

public class InfoText : EntryInfo
{
    [Attr]
    [MaxLength(5000)]
    public string Text { get; set; } = null!;

    [Attr]
    [MaxLength(30)]
    public InfoTextTypeEnum TextType { get; set; }
}

public enum InfoTextTypeEnum
{
    Phone,
    Email,
    Url,
    Note,
}
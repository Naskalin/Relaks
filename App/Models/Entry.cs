using System.ComponentModel.DataAnnotations;
using JsonApiDotNetCore.Resources;
using JsonApiDotNetCore.Resources.Annotations;

namespace App.Models;

[Resource(PublicName = "entries")]
public class Entry : Identifiable<Guid>, ITimestampResource
{
    [Attr]
    [MaxLength(150)]
    [MinLength(2)]
    public string Name { get; set; } = null!;

    [Attr]
    [MaxLength(300)]
    public string? Description { get; set; }

    [Attr(Capabilities = ~AttrCapabilities.AllowChange)]
    public EntryTypeEnum EntryType { get; set; }

    [Attr]
    [Range(1, 10)]
    public int Reputation { get; set; } = 5;

    [Attr]
    public List<EntryTag> Tags { get; set; } = new();
    
    [Attr]
    public List<EntryInfo> Infos { get; set; } = new();

    [Attr(Capabilities = AttrCapabilities.AllowView)]
    public DateTime CreatedAt { get; set; }

    [Attr(Capabilities = AttrCapabilities.AllowView)]
    public DateTime UpdatedAt { get; set; }
    
    [Attr]
    // Дата рождения/начала встречи/создания компании
    public DateTime? StartAt { get; set; }
    
    [Attr]
    // Дата смерти/окончания встречи/закрытия компании
    public DateTime? EndAt { get; set; }
}

public enum EntryTypeEnum
{
    Person,
    Company,
    Meet,
}
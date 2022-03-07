using System.ComponentModel.DataAnnotations;
using JsonApiDotNetCore.Resources;
using JsonApiDotNetCore.Resources.Annotations;

namespace App.Models;

public abstract class Entry : Identifiable<Guid>, ITimestampResource
{
    [Attr]
    [MaxLength(150)]
    public string Name { get; set; } = null!;

    [Attr]
    [MaxLength(200)]
    public string? Description { get; set; }

    [Attr(Capabilities = AttrCapabilities.AllowView)]
    public EntryTypeEnum EntryType { get; set; }

    [Attr]
    [Range(0, 10)]
    public int Reputation { get; set; } = 5;

    [Attr]
    public List<EntryTag> Tags { get; set; } = new();
    
    [Attr]
    public List<EntryInfo> Infos { get; set; } = new();

    [Attr(Capabilities = AttrCapabilities.AllowView)]
    public DateTime CreatedAt { get; set; }

    [Attr(Capabilities = AttrCapabilities.AllowView)]
    public DateTime UpdatedAt { get; set; }
}

[Resource(PublicName = "persons")]
public class Person : Entry
{
}

[Resource(PublicName = "companies")]
public class Company : Entry
{
}

[Resource(PublicName = "meets")]
public class Meet : Entry
{
}

public enum EntryTypeEnum
{
    Person,
    Company,
    Meet,
}
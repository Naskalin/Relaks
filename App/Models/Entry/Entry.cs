using JsonApiDotNetCore.Resources;
using JsonApiDotNetCore.Resources.Annotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace App.Models.Entry;

public abstract class BaseEntry : Identifiable<Guid>
{
    [Attr]
    public string Name { get; set; } = null!;

    [Attr]
    public EntryTypeEnum EntryType { get; set; }

    [Attr]
    public int Reputation { get; set; } = 5;

    [Attr]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

[Resource("persons")]
public class Person : BaseEntry
{
    [Attr]
    public DateTime? BirthDay { get; set; }
}

[Resource("companies")]
public class Company : BaseEntry
{
}

[Resource("meets")]
public class Meet : BaseEntry
{
    [Attr]
    public DateTime StartAt { get; set; }

    [Attr]
    public DateTime? EndAt { get; set; }

    [Attr]
    public string? Address { get; set; }
}

[JsonConverter(typeof(StringEnumConverter))]
public enum EntryTypeEnum
{
    Person,
    Company,
    Meet,
}
using JsonApiDotNetCore.Resources;
using JsonApiDotNetCore.Resources.Annotations;

namespace App.Models;

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

[Resource(PublicName = "persons")]
public class Person : BaseEntry
{
    [Attr]
    public DateTime? BirthDay { get; set; }
}

[Resource(PublicName = "companies")]
public class Company : BaseEntry
{
}

[Resource(PublicName = "meets")]
public class Meet : BaseEntry
{
    [Attr]
    public DateTime StartAt { get; set; }

    [Attr]
    public DateTime? EndAt { get; set; }

    [Attr]
    public string? Address { get; set; }
}

public enum EntryTypeEnum
{
    Person,
    Company,
    Meet,
}
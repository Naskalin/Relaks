using System.ComponentModel.DataAnnotations;
using JsonApiDotNetCore.Resources;
using JsonApiDotNetCore.Resources.Annotations;

namespace App.Models;

[Resource(PublicName = "tags")]
public class EntryTag : Identifiable<Guid>
{
    [Attr]
    [MaxLength(200)]
    public string Title { get; set; } = null!;

    [Attr]
    public List<Entry> Entries { get; set; } = new();
}
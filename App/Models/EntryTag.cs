using System.ComponentModel.DataAnnotations;

namespace App.Models;

public class EntryTag
{
    [Key]
    public Guid Id { get; set; }
    
    [MaxLength(200)]
    public string Title { get; set; } = null!;

    public List<Entry> Entries { get; set; } = new();
    
    public TagTypeEnum TagType { get; set; }
}

public enum TagTypeEnum
{
    Skills,
    Location
}
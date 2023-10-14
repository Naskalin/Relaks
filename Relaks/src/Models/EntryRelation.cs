using System.ComponentModel.DataAnnotations.Schema;
using Relaks.Interfaces;

namespace Relaks.Models;

[Table("EntryRelations")]
public class EntryRelation : IEntryRelation, ITimestamped
{
    public Guid Id { get; set; }
    
    public Guid FirstId { get; set; }
    public BaseEntry First { get; set; } = null!;
    
    public Guid SecondId { get; set; }
    public BaseEntry Second { get; set; } = null!;
    
    public int FirstRating { get; set; }
    public int SecondRating { get; set; }
    public string? Description { get; set; }
    
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }


    // public string? FirstDescription { get; set; }
    // public string? SecondDescription { get; set; }

    public EntryRelation()
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.Now;
        UpdatedAt = DateTime.Now;
    }
}
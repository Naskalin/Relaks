using System.ComponentModel.DataAnnotations;

namespace Relaks.Models;

public class EntryTag
{
    public Guid Id { get; set; } = Guid.NewGuid();
   
    public Guid EntryId { get; set; }
    public BaseEntry Entry { get; set; } = null!;
   
    public Guid TagId { get; set; }
    public EntryTagTitle Tag { get; set; } = null!;
    
    /// <summary>
    /// Необязательное описание тега
    /// </summary>
    [StringLength(255)]
    public string? Description { get; set; }
}
using System.ComponentModel.DataAnnotations;

namespace Relaks.Models;

public class EntryTagTitle
{
   public Guid Id { get; set; } = Guid.NewGuid();
    
   [StringLength(255)]
   public string Title { get; set; } = null!;
    
   public Guid CategoryId { get; set; }
   public EntryTagCategory Category { get; set; } = null!;
}
using System.ComponentModel.DataAnnotations;
using Relaks.Interfaces;

namespace Relaks.Models;

public class EntryTagTitle : ITimestamped
{
   public Guid Id { get; set; } = Guid.NewGuid();
   
   public DateTime CreatedAt { get; set; } = DateTime.Now;
   public DateTime UpdatedAt { get; set; } = DateTime.Now;

   [StringLength(255)]
   public string Title { get; set; } = null!;
    
   public Guid CategoryId { get; set; }
   public EntryTagCategory Category { get; set; } = null!;
}
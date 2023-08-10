using System.ComponentModel.DataAnnotations;

namespace Relaks.Models;

public class Profession
{
    public Guid Id { get; set; }
    
    [StringLength(255)]
    public string Title { get; set; } = null!;
    
    public Guid CategoryId { get; set; }
    public ProfessionCategory Category { get; set; } = null!;
}
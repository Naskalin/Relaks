using System.ComponentModel.DataAnnotations;

namespace Relaks.Models;

public class ProfessionCategory
{
    public Guid Id { get; set; }
    
    [StringLength(255)]
    public string Title { get; set; } = null!;

    public List<Profession> Professions { get; set; } = new();
}
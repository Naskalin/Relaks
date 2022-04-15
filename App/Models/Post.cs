using System.ComponentModel.DataAnnotations;

namespace App.Models;

public class Post
{
    [Key]
    public Guid Id { get; set; }

    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    
    public string AnyField { get; set; } = null!;
}

public class PostFts : IFtsEntity
{
    [Key]
    public int RowId { get; set; }
    public string Match { get; set; } = null!;
    public double? Rank { get; set; }
    
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
}
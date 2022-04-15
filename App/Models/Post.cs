using System.ComponentModel.DataAnnotations;

namespace App.Models;

public class Post
{
    [Key]
    public Guid Id { get; set; }
    public FtsPost FtsPost { get; set; } = null!;

    public string AnyField { get; set; } = null!;
}

public class FtsPost
{
    public string Match { get; set; } = null!;
    public double? Rank { get; set; }

    public Post Post { get; set; } = null!;
    public Guid PostId { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
}
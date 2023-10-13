namespace Relaks.Interfaces;

public interface IEntryRelation
{
    public Guid FirstId { get; set; }
    public Guid SecondId { get; set; }
    public int FirstRating { get; set; }
    public int SecondRating { get; set; }
    public string? FirstDescription { get; set; }
    public string? SecondDescription { get; set; }
}
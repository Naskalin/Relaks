namespace Relaks.Interfaces;

public interface IEntryInfo
{
    public string? Title { get; set; }
    public bool IsFavorite { get; set; }
    public string Discriminator { get; set; }
}
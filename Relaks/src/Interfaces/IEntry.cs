namespace Relaks.Interfaces;

public interface IEntry
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public DateTime? StartAt { get; set; }
    public DateTime? EndAt { get; set; }
    public bool StartAtWithTime { get; set; }
    public bool EndAtWithTime { get; set; }
    public string? Thumbnail { get; set; }
}
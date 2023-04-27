namespace Relaks.Interfaces;

public interface IOrderable
{
    public string? OrderBy { get; set; }
    public bool? IsOrderByDesc { get; set; }
}
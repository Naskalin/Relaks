namespace Relaks.Interfaces;

public interface IPaginatable
{
    public int Page { get; set; }
    public int PerPage { get; set; }
}

public interface IPaginatableResult<T>
{
    public int Page { get; set; }
    public int PerPage { get; set; }
    public List<T> Items { get; set; }
    public int Total { get; set; }
}

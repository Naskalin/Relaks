namespace Relaks.Interfaces;

public interface IPaginatable
{
    public int Page { get; set; }
    public int PerPage { get; set; }
}
public interface ITotal
{
    public int TotalItems { get; set; }
}

public interface IPaginatableResult<T> : ITotalResult<T>
{
    public int Page { get; set; }
    public int PerPage { get; set; }
}
public interface ITotalResult<T> : ITotal
{
    public List<T> Items { get; set; }
}
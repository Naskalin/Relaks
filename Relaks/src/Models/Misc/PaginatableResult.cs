using Relaks.Interfaces;

namespace Relaks.Models.Misc;

public class PaginatableResult<TEntity> : IPaginatableResult<TEntity>
{
    public int Page { get; set; }
    public int PerPage { get; set; }
    public List<TEntity> Items { get; set; } = new();
    public int TotalItems { get; set; }
    public int TotalPages { get; set; }
}

public class TotalResult<TEntity> : ITotalResult<TEntity>
{
    public List<TEntity> Items { get; set; } = new();
    public int TotalItems { get; set; }
}
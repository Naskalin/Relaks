using Relaks.Interfaces;

namespace Relaks.Models.Misc;

public class PaginatableResult<TEntity> : IPaginatableResult<TEntity>
{
    public int Page { get; set; }
    public int PerPage { get; set; }
    public List<TEntity> Items { get; set; } = new();
    public int Total { get; set; }
    public int PageCount { get; set; }
}
using Relaks.Interfaces;

namespace Relaks.Models.Requests;

public class EntryFilterRequest : IPaginatable, IOrderable
{
    public int Page { get; set; }
    public int PerPage { get; set; }
    public string? Discriminator { get; set; }
    public string? OrderBy { get; set; }
    public bool? IsOrderByDesc { get; set; }
}
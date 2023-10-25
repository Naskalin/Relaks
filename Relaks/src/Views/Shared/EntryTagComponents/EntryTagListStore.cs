using Relaks.Database;
using Relaks.Database.Repositories;
using Relaks.Models;

namespace Relaks.Views.Shared.EntryTagComponents;

public class EntryTagListRequest
{
    public string? Search { get; set; }
    public Guid? CategoryId { get; set; }
}

public class EntryTagListStore
{
    public enum SidebarStateEnum
    {
        Default,
        EditCategory,
        EditCategoryEmpty,
        NewCategory,
    }
    
    private readonly AppDbContext _db;
    public EntryTagListRequest Req { get; set; } = new();
    public Guid? SelectedCategoryIdRoot { get; set; }
    public SidebarStateEnum SidebarState { get; set; } = SidebarStateEnum.Default;

    public EntryTagListStore(AppDbContext db)
    {
        _db = db;
    }

    public List<EntryTagCategory> Categories { get; set; } = new();
    public List<EntryTagTitle> Tags { get; set; } = new();

    public void Initialize()
    {
        Categories = _db.EntryTagCategories.ToBaseTree();
        Tags = _db.EntryTagTitles.OrderByDescending(x => x.UpdatedAt).Take(20).ToList();
        Req = new EntryTagListRequest();
    }

    public void FindByRequest()
    {
        var q = _db.EntryTagTitles.AsQueryable();
        if (!string.IsNullOrEmpty(Req.Search))
            q = q.Where(x => x.Title.ToLower().Contains(Req.Search.ToLower()));

        if (Req.CategoryId.HasValue)
            q = q.Where(x => x.CategoryId.Equals(Req.CategoryId.Value));

        Tags = q.ToList();
    }
}
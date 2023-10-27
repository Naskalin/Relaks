using Microsoft.EntityFrameworkCore;
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
        NewCategory,
    }
    
    public enum TagsStateEnum
    {
        Default,
        EditTag,
        NewTag,
    }
    
    private readonly AppDbContext _db;
    public EntryTagListRequest Req { get; set; } = new();
    public Guid? SelectedCategoryIdRoot { get; set; }
    public SidebarStateEnum SidebarState { get; set; }
    public TagsStateEnum TagsState { get; set; }
    public Guid? EditCategoryId { get; set; }
    public Guid? EditTagTitleId { get; set; }

    public EntryTagListStore(AppDbContext db)
    {
        _db = db;
    }

    public Dictionary<Guid, int> CategoryTagsCount { get; set; } = new();
    public List<EntryTagCategory> Categories { get; set; } = new();
    public List<EntryTagTitle> Tags { get; set; } = new();

    public void Initialize()
    {

        SidebarState = SidebarStateEnum.Default;
        TagsState = TagsStateEnum.Default;
        FindTagsCategoriesCount();
        Categories = _db.EntryTagCategories
            .Include(x => x.Tags)
            .ToBaseTree();
        Req = new EntryTagListRequest();
        FindTags();
    }

    public void FindTagsCategoriesCount()
    {
        CategoryTagsCount = _db
            .EntryTagCategories
            .Include(x => x.Tags)
            .Where(x => x.Tags.Any())
            .ToDictionary(x => x.Id, x => x.Tags.Count);
    }

    public void FindTags()
    {
        var q = _db.EntryTagTitles.AsQueryable();
        if (Req.CategoryId.HasValue || !string.IsNullOrEmpty(Req.Search))
        {
            if (!string.IsNullOrEmpty(Req.Search))
            {
                q = q.Where(x => x.Title.Contains(Req.Search));
            }

            if (Req.CategoryId.HasValue)
            {
                q = q.Include(x => x.Category);
                q = q.Where(x => x.CategoryId.Equals(Req.CategoryId.Value) || x.Category.TreePath.Contains(Req.CategoryId.Value.ToString()));
            }
            
            q = q.OrderBy(x => x.Title);
        }
        else
        {
            q = _db.EntryTagTitles.OrderByDescending(x => x.UpdatedAt);
        }
        
        Tags = q.Distinct().Take(100).ToList();
    }
}
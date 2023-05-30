using Relaks.Database;
using Relaks.Database.Repositories;
using Relaks.Models.Misc;

namespace Relaks.Models.Store;

public class AppFileListStore
{
    private readonly AppDbContext _db;

    public AppFileListStore(AppDbContext db)
    {
        _db = db;
        SidebarState = SidebarStateEnum.Default;
    }

    public enum SidebarStateEnum
    {
        Default,
        
        AddTag,
        EditTag,
        EditTags,
        
        AddCategory,
        EditCategory,
        EditCategories,
    }

    public enum BodyStateEnum
    {
        Default,
        EditFile,
        MassChangeCategory,
        // ChangeTags,
    }

    public Guid? EntryId { get; set; }
    public bool? WithEdit { get; set; }
    public SidebarStateEnum SidebarState { get; set; }
    public Guid? SidebarEditId { get; set; }
    public BodyStateEnum BodyState { get; set; }
    public Guid? BodyEditId { get; set; }
    public AppFileFindRequest Req { get; set; } = new();
    public TotalResult<AppFileFindResult> ResultFiles { get; set; } = new();
    public List<BaseFileTag> Tags { get; set; } = new();
    public List<BaseFileCategory> Categories { get; set; } = new();
    public List<Guid> SelectedFileIds { get; set; } = new();

    public void FindFiles()
    {
        ResultFiles = _db.FindFiles(Req);
        if (BodyState == BodyStateEnum.Default)
        {
            SelectedFileIds.Clear();   
        }
    }

    public void GetTags()
    {
        if (Req.EntryId.HasValue)
        {
            Tags = _db.EntryFileTags
                .AsEnumerable()
                .Where(x => x.EntryId.Equals(Req.EntryId.Value))
                .OrderBy(x => x.Title, StringComparer.OrdinalIgnoreCase)
                .Select(x => (BaseFileTag) x)
                .ToList();
        }
    }

    public void GetCategories()
    {
        var q = _db.BaseFileCategories.AsQueryable();
        if (Req.EntryId.HasValue)
        {
            var categoryIds = _db.EntryFileCategories.Where(x => x.EntryId.Equals(Req.EntryId.Value)).Select(x => x.Id);
            q = q.Where(x => categoryIds.Contains(x.Id));
        }
        
        Categories = q.ToTree();
    }
}
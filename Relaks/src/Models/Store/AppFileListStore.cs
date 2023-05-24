using BootstrapBlazor.Components;
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
        State = StateEnum.Default;
    }

    public enum StateEnum
    {
        Default,
        
        AddTag,
        EditTag,
        EditTags,
        
        AddCategory,
        EditCategory,
        EditCategories,
    }

    public Guid? EntryId { get; set; }
    public bool? WithEdit { get; set; }
    public StateEnum State { get; set; }
    public Guid? EditId { get; set; }
    public AppFileFindRequest Req { get; set; } = new();
    public TotalResult<AppFileFindResult> ResultFiles { get; set; } = new();
    public List<BaseFileTag> Tags { get; set; } = new();
    public List<BaseFileCategory> Categories { get; set; } = new();

    public void FindFiles() => ResultFiles = _db.FindFiles(Req);

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
        if (Req.EntryId.HasValue)
        {
            Categories = _db.EntryFileCategories.Where(x => x.EntryId.Equals(Req.EntryId.Value))
                .AsEnumerable()
                .Where(x => x.EntryId.Equals(Req.EntryId.Value))
                .OrderBy(x => x.Title, StringComparer.OrdinalIgnoreCase)
                .Select(x => (BaseFileCategory) x)
                .ToList();
        }
    }
}
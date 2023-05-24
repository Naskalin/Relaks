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
        AddCategory,
        AddTag,
        EditTag,
        EditCategory,
    }
    
    
    public Guid? EntryId { get; set; }
    public bool? WithEdit { get; set; }
    public StateEnum State { get; set; }
    public Guid? EditId { get; set; }
    public AppFileFindRequest Req { get; set; } = new();
    public TotalResult<AppFileFindResult> Result { get; set; } = new();
    
    public void FindData() => Result = _db.FindFiles(Req);
}
namespace Relaks.Models;

public abstract class BaseFileCategory
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string Discriminator { get; set; } = null!;
    
    public BaseFileCategory? Parent { get; set; }
    public Guid? ParentId { get; set; }
    
    public List<BaseFileCategory> Children = new();

    protected BaseFileCategory()
    {
        Id = Guid.NewGuid();
    }
}

public class EntryFileCategory : BaseFileCategory
{
    public Guid EntryId { get; set; }
    public BaseEntry Entry { get; set; } = null!;
}

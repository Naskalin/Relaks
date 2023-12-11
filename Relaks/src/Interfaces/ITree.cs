namespace Relaks.Interfaces;

public interface ITree<TEntity> where TEntity : class, ITree<TEntity>
{
    public Guid Id { get; set; }
    public List<TEntity> Children { get; set; }
    public Guid? ParentId { get; set; }
    public TEntity? Parent { get; set; }
    public string TreePath { get; set; }
    public string Title { get; set; }
}

public interface IParentable
{
    public Guid Id { get; set; }
    public Guid? ParentId { get; set; }
}
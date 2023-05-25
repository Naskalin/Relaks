using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Relaks.Database;
using Relaks.Interfaces;
using Relaks.Managers;

namespace Relaks.Validators;

public class TreeValidator<TEntity> : AbstractValidator<TEntity> where TEntity : class, ITree<TEntity>
{
    public TreeValidator(AppDbContext db)
    {
        When(x => x.ParentId != null, () =>
        {
            RuleFor(x => x.ParentId).NotEqual(x => x.Id).NotEqual(default(Guid)).WithMessage("Узел не может быть родительским для самого себя");
            RuleFor(x => x).Must(x => false == HasRecursion(db.Set<TEntity>(), x));
        });
    }

    private static bool HasRecursion(IQueryable<ITree<TEntity>> q, ITree<TEntity> node)
    {
        // Нет родителя = нет рекурсии
        if (!node.ParentId.HasValue) return false;
        
        var existNode = q.FirstOrDefault(x => x.Id.Equals(node.Id));
        // Если ещё не в бд, то рекурсии не будет
        if (existNode == null) return false;
        if (existNode.Id.Equals(existNode.ParentId)) return true;

        // flat tree
        var tree = q
            .Where(x => x.TreePath.Contains(existNode.Id.ToString()))
            .Include(x => x.Children)
            .ToList();

        // normalize tree
        var nodeWithChildren = tree.First(x => x.Id.Equals(node.Id));
        
        var child = TreeManager.FindChildById(nodeWithChildren, node.ParentId.Value);
        return child != null;
    }
}
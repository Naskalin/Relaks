using FluentValidation;
using LinqKit;
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
            RuleFor(x => x).Must(x => false == HasRecursion(db.Set<TEntity>(), x)).WithMessage("Рекурсия. Родительский узел не может быть вложен в дочерний.");
        });
    }

    private static bool HasRecursion(IQueryable<ITree<TEntity>> q, ITree<TEntity> node)
    {
        // Нет родителя = нет рекурсии
        if (!node.ParentId.HasValue) return false;
        
        // Id == parentId = рекурсия
        if (node.Id.Equals(node.ParentId)) return true;
        
        // Нода ещё не в бд, то рекурсии не будет
        var existNode = q.FirstOrDefault(x => x.Id.Equals(node.Id));
        if (existNode == null) return false;
        
        var parent = q.FirstOrDefault(x => x.Id.Equals(node.ParentId));
        ArgumentNullException.ThrowIfNull(parent);

        // Node.Parent
        // -- Node.Children
        // ---- Node.SubChildren
        // Предполагаем, что Node.Parent пытается выбрать в качестве родителя Node.Children или Node.SubChildren
        // Нужно проверить, есть ли в пути выбираемой в качестве родителя ноды Id Node.Parent, то есть Id текущей ноды
        return parent
            .TreePath.Split("/")
            .Where(x => !string.IsNullOrEmpty(x))
            .Any(x => Guid.Parse(x).Equals(node.Id));
        
        // // flat tree
        // var tree = q
        //     .Where(x => x.TreePath.Contains(node.Id.ToString()))
        //     .Include(x => x.Children)
        //     .ToList();
        //
        // // normalize tree
        // var nodeWithChildren = tree.First(x => x.Id.Equals(node.Id));
        //
        // var child = TreeManager.FindChildById(nodeWithChildren, node.ParentId.Value);
        // return child != null;
    }
}
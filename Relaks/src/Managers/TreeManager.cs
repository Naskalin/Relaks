using Microsoft.EntityFrameworkCore;
using Relaks.Database;
using Relaks.Interfaces;

namespace Relaks.Managers;

public static class TreeManager
{

    /// <summary>
    /// Поиск ноды в детях текущей ноды по идентификатору
    /// </summary>
    /// <returns>Вернёт ноду, если она будет найдена</returns>
    public static ITree<TEntity>? FindChildById<TEntity>(ITree<TEntity> node, Guid searchId) where TEntity : class, ITree<TEntity>
    {
        if (node.Id.Equals(searchId))
            return node;
        
        foreach (var child in node.Children)
        {
            var existChildNode = FindChildById(child, searchId);
            if (existChildNode != null) return existChildNode;
        }
        
        return null;
    }

    public static void SyncTreePaths<TEntity>(IQueryable<TEntity> q, Guid nodeId, TEntity model) where TEntity : class, ITree<TEntity>
    {
        // New node, not found in db
        if (!q.Any(x => x.Id.Equals(nodeId))) return;
        
        // flat tree
        var allTree = q
            .Include(x => x.Children)
            .ToList();
        
        // normalize tree to root nodes
        allTree = allTree.Where(x => x.ParentId.Equals(null)).ToList();
        
        foreach (var rootNode in allTree)
        {
            SyncChildPaths(rootNode, rootNode.Children);
        }
    }
    
    private static void SyncChildPaths<TEntity>(TEntity parentNode, List<TEntity> children, string? parentPath = null) where TEntity : class, ITree<TEntity>
    {
        if (parentNode.ParentId == null) UpdateTreePath(parentNode);
        foreach (var child in children)
        {
            UpdateTreePath(child, parentNode);
            SyncChildPaths(child, child.Children, parentPath);
        }
    }
    
    public static void UpdateTreePath<TEntity>(ITree<TEntity> child, ITree<TEntity>? parentNode = null) where TEntity : class, ITree<TEntity>
    {
        string pathParentNode = parentNode?.TreePath ?? "";
        child.TreePath = $"{pathParentNode.ToLower()}/{child.Id.ToString().ToLower()}";
    }
    
    public static List<(string Title, Guid Value, int Level)> ToTreeSelect<TEntity>(this List<TEntity> tree, List<(string Title, Guid Value, int Level)>? result = null, int? level = null) where TEntity : class, ITree<TEntity>
    {
        result ??= new();
        level = level.HasValue ? level.Value + 1 : 0;
    
        var dashes = new List<string>();
        for (int i = 0; i < level.Value; i++)
        {
            dashes.Add("—");
        }
    
        var beforeTitle = string.Join(" ", dashes);
        if (dashes.Any()) beforeTitle += " ";
    
        foreach (var category in tree)
        {
            result.Add((beforeTitle + category.Title, category.Id, level.Value));
            category.Children.ToTreeSelect(result, level);
        }
    
        return result;
    }
}
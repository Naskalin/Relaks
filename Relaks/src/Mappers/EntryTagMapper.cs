using Relaks.Models;

namespace Relaks.Mappers;

public static class EntryTagMapper
{
    public static void MapTo(this EntryTagCategory from, EntryTagCategory to)
    {
        to.Id = from.Id;
        to.ParentId = from.ParentId;
        to.Title = from.Title;
    }
    
    public static void MapTo(this EntryTagTitle from, EntryTagTitle to)
    {
        to.Id = from.Id;
        to.CategoryId = from.CategoryId;
        to.Title = from.Title;
        to.CreatedAt = from.CreatedAt;
        to.UpdatedAt = from.UpdatedAt;
    }

    // public static void MapTo(this EntryTagCategory from, EntryTagCategoryDto to)
    // {
    //     to.Id = from.Id;
    //     to.ParentId = from.ParentId;
    //     to.TreePath = from.TreePath;
    //     to.Title = from.Title;
    //     
    //     if (from.Parent != null)
    //     {
    //         var parent = new EntryTagCategoryDto();
    //         from.Parent.MapTo(parent);
    //         to.Parent = parent;
    //     }
    //
    //     var children = new List<EntryTagCategoryDto>();
    //     foreach (var child in from.Children)
    //     {
    //         var dto = new EntryTagCategoryDto();
    //         child.MapTo(dto);
    //         children.Add(dto);
    //     }
    //
    //     to.Children = children;
    // }
}
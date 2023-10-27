using System.ComponentModel.DataAnnotations;
using Relaks.Interfaces;

namespace Relaks.Models;

public class EntryTagCategory : ITree<EntryTagCategory>
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public List<EntryTagCategory> Children { get; set; } = new();
    public Guid? ParentId { get; set; }
    public EntryTagCategory? Parent { get; set; }
    public string TreePath { get; set; } = null!;

    [StringLength(255)]
    public string Title { get; set; } = null!;

    public List<EntryTagTitle> Tags { get; set; } = new();
}

public class EntryTagCategoryDto : ITree<EntryTagCategoryDto>
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public List<EntryTagCategoryDto> Children { get; set; } = new();
    public Guid? ParentId { get; set; }
    public EntryTagCategoryDto? Parent { get; set; }
    public string TreePath { get; set; } = null!;
    public string Title { get; set; } = null!;
    public int TagsCount { get; set; }
}
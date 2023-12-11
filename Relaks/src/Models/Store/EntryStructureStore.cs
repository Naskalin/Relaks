using Relaks.Database;
using Relaks.Database.Repositories;
using Relaks.Models.StructureModels;

namespace Relaks.Models.Store;

public class StructureDiagramOptions
{
    public enum ShowItemsTypeEnum
    {
        Show,
        Hide,
        Some
    }
    public int DiagramScale { get; set; } = 100;
    public int LimitStructureItems { get; set; } = 3;
    public ShowItemsTypeEnum ShowItemsType { get; set; } = ShowItemsTypeEnum.Some;
    public bool IsShowDates { get; set; }
}

public class EntryStructureStore(AppDbContext db)
{
    public enum SidebarStateEnum
    {
        Default,
        GroupEdit,
        GroupCreate,
        ItemCreate,
        ItemEdit,
    }
    public StructureDiagramOptions DiagramOptions { get; set; } = new();
    
    public StructureGroup? SelectedGroup { get; set; }
    public StructureItem? SelectedItem { get; set; }
    public string Discriminator { get; set; } = null!;
    public List<StructureGroup> StructureGroups { get; set; } = new();
    public StructureGroupListRequest Req { get; set; } = new();
    public SidebarStateEnum SidebarState { get; set; } = SidebarStateEnum.Default;
    
    /// <summary>
    /// Переход на страницу был осуществлён со страницы /connections
    /// Нужно некоторымы способами оповестить об этом
    /// </summary>
    public StructureItem? BackToConnectionsStructureItem { get; set; }

    public void FindStructure()
    {
        StructureGroups = db.StructureGroups.ToTree(Req);
    }
}
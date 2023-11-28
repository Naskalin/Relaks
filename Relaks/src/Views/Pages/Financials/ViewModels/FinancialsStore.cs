namespace Relaks.Views.Pages.Financials.ViewModels;

public class FinancialsStore
{
    public enum SidebarEnum
    {
        Default,
        AddAccount,
        EditAccount,
        AddAccountCategory,
        EditAccountCategory,
    }

    public SidebarEnum SidebarState { get; set; } = SidebarEnum.Default;
    public Guid? SidebarEditAccountCategoryId { get; set; }
    public Guid? SidebarEditAccountId { get; set; }
}
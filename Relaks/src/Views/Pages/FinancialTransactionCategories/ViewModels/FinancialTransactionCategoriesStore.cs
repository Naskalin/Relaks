using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components;
using Relaks.Database;
using Relaks.Database.Repositories;
using Relaks.Models.FinancialModels;
using Relaks.src.Views.Pages.FinancialTransactionCategories;

namespace Relaks.Views.Pages.FinancialTransactionCategories.ViewModels;

public class FinancialTransactionCategoriesStore(AppDbContext db, DialogService dialogService)
{
    public enum SidebarStateEnum
    {
        New,
        Edit,
    }
    
    public Guid? CategoryId { get; set; }
    public List<FinancialTransactionCategory> TransactionCategories { get; set; } = new();
    public SidebarStateEnum SidebarState { get; set; } = SidebarStateEnum.New;

    public void FindCategories()
    {
        TransactionCategories = db.FinancialTransactionCategories.ToBaseTree();
    }
    
    private void HandleFormSubmit()
    {
        FindCategories();
        CategoryId = null;
    }

    private Task OnCloseAsync()
    {
        CategoryId = null;
        return Task.CompletedTask;
    }
    
    public Task ShowFormModal()
    {
        var option = new DialogOption
        {
            IsKeyboard = true,
            IsBackdrop = true,
            Title = CategoryId.HasValue ? "Изменить категорию транзакций" : "Добавить категорию для транзакций",
            IsScrolling = true,
            CloseButtonText = "Закрыть",
            Size = BootstrapBlazor.Components.Size.Large,
            OnCloseAsync = OnCloseAsync
        };
        
        option.Component = BootstrapDynamicComponent.CreateComponent<_FinancialTransactionCategoriesForm>(new Dictionary<string, object?>()
        {
            ["CategoryId"] = CategoryId,
            ["OnFormSubmit"] = EventCallback.Factory.Create(this, async _ =>
            {
                HandleFormSubmit();
                await option.CloseDialogAsync();
            }),
        });
        
        return dialogService.Show(option);
    }
}
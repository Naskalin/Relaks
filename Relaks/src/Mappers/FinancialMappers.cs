using Relaks.Models.FinancialModels;
using Relaks.Views.Pages.Financials.ViewModels;

namespace Relaks.Mappers;

public static class FinancialMappers
{
    public static void MapTo(this FinancialAccountCategory category, FinancialAccountCategoryRequest req)
    {
        req.Title = category.Title;
    }
    
    public static void MapTo(this FinancialAccountCategoryRequest req, FinancialAccountCategory category)
    {
        category.Title = req.Title;
    }

    public static void MapTo(this FinancialAccount account, FinancialAccountRequest req)
    {
        req.Title = account.Title;
        req.Description = account.Description;
        req.FinancialCurrencyId = account.FinancialCurrencyId;
        req.CategoryId = account.CategoryId;
        req.StartAt = account.StartAt;
        req.EndAt = account.EndAt;
        req.EntryId = account.EntryId;
    }
    
    public static void MapTo(this FinancialAccountRequest req, FinancialAccount account)
    {
        account.Title = req.Title;
        account.Description = req.Description;
        account.FinancialCurrencyId = req.FinancialCurrencyId;
        account.CategoryId = req.CategoryId;
        account.StartAt = req.StartAt;
        account.EndAt = req.EndAt;
        
        ArgumentNullException.ThrowIfNull(req.EntryId);
        account.EntryId = req.EntryId.Value;
    }
}
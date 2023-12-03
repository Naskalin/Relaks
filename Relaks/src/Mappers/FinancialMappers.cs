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
        req.InitialBalance = account.InitialBalance;
    }
    
    public static void MapTo(this FinancialAccountRequest req, FinancialAccount account)
    {
        account.Title = req.Title;
        account.Description = req.Description;
        account.FinancialCurrencyId = req.FinancialCurrencyId;
        account.CategoryId = req.CategoryId;
        account.StartAt = req.StartAt;
        account.EndAt = req.EndAt;
        account.InitialBalance = req.InitialBalance;
        
        ArgumentNullException.ThrowIfNull(req.EntryId);
        account.EntryId = req.EntryId.Value;
    }

    public static void MapTo(this FinancialTransactionRequest req, FinancialTransaction transaction)
    {
        ArgumentNullException.ThrowIfNull(req.EntryId);
        ArgumentNullException.ThrowIfNull(req.IsPlus);
        
        transaction.IsPlus = req.IsPlus.Value;
        transaction.CreatedAt = req.CreatedAt;
        transaction.Description = req.Description;
        transaction.AccountId = req.AccountId;
        transaction.EntryId = req.EntryId.Value;
    }
    
    public static void MapTo(this FinancialTransaction transaction, FinancialTransactionRequest req)
    {
        req.IsPlus = transaction.IsPlus;
        req.CreatedAt = transaction.CreatedAt;
        req.Description = transaction.Description;
        req.AccountId = transaction.AccountId;
        req.EntryId = transaction.EntryId;
    }

    public static void MapTo(this FinancialTransactionItemRequest req, FinancialTransactionItem item)
    {
        item.CategoryId = req.CategoryId;
        item.Quantity = req.Quantity;
        item.Amount = req.Amount;
        item.Description = req.Description;
    }
    
    public static void MapTo(this FinancialTransactionItem item, FinancialTransactionItemRequest req)
    {
        req.CategoryId = item.CategoryId;
        req.Quantity = item.Quantity;
        req.Amount = item.Amount;
        req.Description = item.Description;
    }
}
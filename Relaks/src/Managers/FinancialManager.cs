using Microsoft.EntityFrameworkCore;
using Relaks.Database;
using Relaks.Mappers;
using Relaks.Models.FinancialModels;
using Relaks.Views.Pages.EntryFinancials.ViewModels;

namespace Relaks.Managers;

public partial class FinancialManager(AppDbContext db)
{
    
    private static void CreateItemsForTransaction(BaseFinancialTransaction transaction, List<FinancialTransactionItemRequest> reqItems)
    {
        var newItems = reqItems
            .Where(x => !x.Id.HasValue) // new items has no id
            .Select(x =>
            {
                var item = new FinancialTransactionItem();
                x.MapTo(item);
                return item;
            }).ToList();

        transaction.Items.AddRange(newItems);
    }

    private static void DeleteItemsForTransaction(BaseFinancialTransaction transaction, List<FinancialTransactionItemRequest> reqItems)
    {
        var reqIds = reqItems.Where(x => x.Id.HasValue).Select(x => x.Id!.Value).ToList();
        transaction.Items.RemoveAll(x => !reqIds.Contains(x.Id));
    }

    private static void UpdateItemsForTransaction(BaseFinancialTransaction transaction, List<FinancialTransactionItemRequest> reqItems)
    {
        reqItems.Where(x => x.Id.HasValue).ToList().ForEach(reqItem =>
        {
            var item = transaction.Items.First(t => t.Id.Equals(reqItem.Id));
            reqItem.MapTo(item);
        });
    }

    private BaseFinancialTransaction? PreviousTransaction { get; set; }
    
    /// <summary>
    /// Отсортированные транзакции
    /// </summary>
    /// <param name="orderedTransactions">Отсортированные транзакции в порядке возрастания</param>
    /// <param name="fromBalance">Начальный баланс для первой транзакции</param>
    private void UpdateBalanceForTransactions(List<BaseFinancialTransaction> orderedTransactions, decimal fromBalance)
    {
        PreviousTransaction = null;

        foreach (var transaction in orderedTransactions)
        {
            transaction.UpdateBalance(PreviousTransaction?.Balance ?? fromBalance);
            PreviousTransaction = transaction;
        }
    }
}
using Microsoft.EntityFrameworkCore;
using Relaks.Models.FinancialModels;

namespace Relaks.Managers;

public partial class FinancialManager
{
    public void DeleteTransaction(AccountFinancialTransaction transaction)
    {
        // var reverseTransaction = transaction.ReverseTransaction;
        var reverseTransaction = db.AccountFinancialTransactions.First(x => x.Id.Equals(transaction.ReverseTransactionId));
        BaseDeleteTransaction(transaction);
        BaseDeleteTransaction(reverseTransaction);
    }
    
    public void DeleteTransaction(EntryFinancialTransaction transaction)
    {
        BaseDeleteTransaction(transaction);
    }
    
    public void BaseDeleteTransaction(BaseFinancialTransaction transaction)
    {
        // 9:45---10:00(Удаляемая)---10:15---10:30
        db.BaseFinancialTransactions.Remove(transaction);
        
        var account = db.FinancialAccounts.First(x => x.Id.Equals(transaction.AccountId));
        var transactionsQuery = db
                .BaseFinancialTransactions
                .Include(x => x.Items)
                .OrderBy(x => x.CreatedAt)
                .Where(x => x.AccountId.Equals(transaction.AccountId))
            ;
        
        var otherTransactions = transactionsQuery
                .Where(x => x.CreatedAt >= transaction.CreatedAt)
                .Where(x => !x.Id.Equals(transaction.Id))
                .ToList()
            ;
        
        if (otherTransactions.Any())
        {
            UpdateBalanceForTransactions(otherTransactions, transaction.FromBalance());
            account.Balance = otherTransactions.Last().Balance;
            return;
        }

        account.Balance = transaction.FromBalance();
    }
}
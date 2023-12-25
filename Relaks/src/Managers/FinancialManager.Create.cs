using Microsoft.EntityFrameworkCore;
using Relaks.Mappers;
using Relaks.Models.FinancialModels;
using Relaks.Views.Pages.EntryFinancials.ViewModels;

namespace Relaks.Managers;

public partial class FinancialManager
{
    public void CreateTransaction(AccountFinancialTransactionRequest req)
    {
        var transaction = new AccountFinancialTransaction();
        var reverseTransaction = new AccountFinancialTransaction();
        transaction.ReverseTransactionId = reverseTransaction.Id;
        reverseTransaction.ReverseTransactionId = transaction.Id;
        
        req.MapTo(transaction);
        CreateItemsForTransaction(transaction, req.Items);
        transaction.UpdateTotal();
        UpdateBalanceForNewTransaction(transaction);
        db.AccountFinancialTransactions.Add(transaction);

        var reverseReq = req.ToReverseRequest();
        reverseReq.MapTo(reverseTransaction);
        CreateItemsForTransaction(reverseTransaction, reverseReq.Items);
        reverseTransaction.UpdateTotal();
        UpdateBalanceForNewTransaction(reverseTransaction);
        db.AccountFinancialTransactions.Add(reverseTransaction);
    }
    
    public void CreateTransaction(EntryFinancialTransactionRequest req)
    {
        var transaction = new EntryFinancialTransaction();
        req.MapTo(transaction);
        CreateItemsForTransaction(transaction, req.Items);
        transaction.UpdateTotal();
        UpdateBalanceForNewTransaction(transaction);
        db.EntryFinancialTransactions.Add(transaction);
    }
    
    private void UpdateBalanceForNewTransaction(BaseFinancialTransaction newTransaction)
    {
        var account = db.FinancialAccounts.First(x => x.Id.Equals(newTransaction.AccountId));
        var transactionsQuery = db
                .BaseFinancialTransactions
                .Include(x => x.Items)
                .OrderByDescending(x => x.CreatedAt)
                .Where(x => x.AccountId.Equals(newTransaction.AccountId))
            ;

        // Новая транзакция добавиться до последней
        // Нужно обновить баланс этой и всех последующих транзакций и аккаунта
        // Начальный баланс берём из первой транзакции, которая идёт до этой
        // 9:45---10:00(Новая)---10:15---10:30
        // 9:45 < 10:00 - самая свежая
        // others >= 10:00
        // Предыдущая транзакция (из неё можно взять начальный баланс)
        var earlierTransaction = transactionsQuery.FirstOrDefault(x => x.CreatedAt < newTransaction.CreatedAt);

        // Остальные транзакции, пойдут после текущей в порядке возрастания по дате
        var otherTransactions = transactionsQuery
            .Where(x => x.CreatedAt >= newTransaction.CreatedAt)
            .OrderBy(x => x.CreatedAt)
            .ToList();
        if (earlierTransaction == null)
        {
            // Если не найдена такая траназкция
            if (otherTransactions.Any())
            {
                // Если есть другие транзакции, идущие после новой
                // Начальный баланс берём из самой первой транзакции, которая идёт сразу после новой
                // Т.е. мы как бы переносим этот начальный баланс в эту транзакцию
                newTransaction.UpdateBalance(otherTransactions.First().FromBalance());
                UpdateBalanceForTransactions(otherTransactions, newTransaction.Balance);
                account.Balance = otherTransactions.Last().Balance;
                return;
            }

            // Получается, что это самая первая транзакция
            // Нет ранней транзакции и нет поздних транзакций
            newTransaction.UpdateBalance(account.Balance);
            account.Balance = newTransaction.Balance;
            return;
        }

        // Если найдена такая транзакция
        newTransaction.UpdateBalance(earlierTransaction.Balance);
        if (otherTransactions.Any())
        {
            // Обновляем балансы последующих транзакций
            // Начальный баланс берём из новой транзакции, которая "встанет между ними" по времени
            UpdateBalanceForTransactions(otherTransactions, newTransaction.Balance);
            // Баланс для счёта берём из последней транзакции
            account.Balance = otherTransactions.Last().Balance;
            return;
        }

        // Последующих транзакций не найдено, баланс для счёта берём из новой транзакции
        account.Balance = newTransaction.Balance;
    }
}
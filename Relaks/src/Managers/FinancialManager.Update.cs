using Microsoft.EntityFrameworkCore;
using Relaks.Mappers;
using Relaks.Models.FinancialModels;
using Relaks.Views.Pages.EntryFinancials.ViewModels;

namespace Relaks.Managers;

public partial class FinancialManager
{
    public void UpdateTransaction(AccountFinancialTransaction transaction, AccountFinancialTransactionRequest req)
    {
        // Удаляем текущую реверс транзакцию
        // DeleteTransaction(transaction.ReverseTransaction);
        // var reverseTransaction = db.BaseFinancialTransactions.First(x => x.Id.Equals(transaction.ReverseTransactionId));
        BaseDeleteTransaction(db.AccountFinancialTransactions.First(x => x.Id.Equals(transaction.ReverseTransactionId)));
        // Создаём новую реверс транзакцию
        var reverseTransaction = new AccountFinancialTransaction();
        
        var initialFromBalance = transaction.FromBalance();
        var initialCreatedAt = transaction.CreatedAt;
        req.MapTo(transaction);
        // Связываем изменяемую транзакцию с новой реверсивной
        // transaction.ReverseTransaction = reverseTransaction;
        transaction.ReverseTransactionId = reverseTransaction.Id;
        DeleteItemsForTransaction(transaction, req.Items);
        UpdateItemsForTransaction(transaction, req.Items);
        CreateItemsForTransaction(transaction, req.Items);
        transaction.UpdateTotal();
        UpdateBalanceForExistingTransaction(transaction, initialFromBalance, initialCreatedAt);
        
        reverseTransaction.ReverseTransactionId = transaction.Id;
        // Получаем реверс реквест
        var reverseReq = req.ToReverseRequest();
        // изменяем его так, чтобы создалист новые элементы (item.Id = null)
        reverseReq.Items = transaction.Items.Select(x =>
        {
            var itemReq = new FinancialTransactionItemRequest();
            x.MapTo(itemReq);
            return itemReq;
        }).ToList();
        reverseReq.MapTo(reverseTransaction);
        
        CreateItemsForTransaction(reverseTransaction, reverseReq.Items);
        reverseTransaction.UpdateTotal();
        UpdateBalanceForNewTransaction(reverseTransaction);
        db.AccountFinancialTransactions.Add(reverseTransaction);
    }
    
    public void UpdateTransaction(EntryFinancialTransaction transaction, EntryFinancialTransactionRequest req)
    {
        var initialFromBalance = transaction.FromBalance();
        var initialCreatedAt = transaction.CreatedAt;
        req.MapTo(transaction);
        DeleteItemsForTransaction(transaction, req.Items);
        UpdateItemsForTransaction(transaction, req.Items);
        CreateItemsForTransaction(transaction, req.Items);
        transaction.UpdateTotal();
        UpdateBalanceForExistingTransaction(transaction, initialFromBalance, initialCreatedAt);
    }
    
    /// <summary>
    /// Обновляем все балансы в соответствии с новой транзакцией
    /// </summary>
    /// <param name="editingTransaction">Транзакция, до её сохранения в базе данных</param>
    /// <param name="initialFromBalance">Исходный FromBalance(), до изменения модели</param>
    /// <param name="initialCreatedAt">Исходная дата, до изменения модели</param>
    private void UpdateBalanceForExistingTransaction(BaseFinancialTransaction editingTransaction, decimal initialFromBalance, DateTime initialCreatedAt)
    {
        var account = db.FinancialAccounts.First(x => x.Id.Equals(editingTransaction.AccountId));
        var transactionsQuery = db
                .BaseFinancialTransactions
                .Include(x => x.Items)
                .OrderByDescending(x => x.CreatedAt)
                .Where(x => x.AccountId.Equals(editingTransaction.AccountId))
            ;

        if (initialCreatedAt.Equals(editingTransaction.CreatedAt))
        {
            // Время транзакции не изменено
            var otherTransactions = transactionsQuery
                .Where(x => x.CreatedAt >= initialCreatedAt)
                .OrderBy(x => x.CreatedAt)
                .ToList();
            
            UpdateBalanceForTransactions(otherTransactions, initialFromBalance);
            account.Balance = otherTransactions.Last().Balance;
            return;
        }

        // Время транзакции было изменено
        // 9:45---10:00(Было)---10:15---10:30(Стало)--10:45
        if (editingTransaction.CreatedAt > initialCreatedAt)
        {
            // Последующие транзакции
            // Всё что позднее >= 10:00(Было)
            var otherTransactions = transactionsQuery
                .Where(x => x.CreatedAt >= initialCreatedAt)
                .OrderBy(x => x.CreatedAt)
                .ToList();
            
            if (otherTransactions.Any())
            {
                // если есть последующие, то обновляем их балансы
                UpdateBalanceForTransactions(otherTransactions, initialFromBalance);
                // и задаём баланс счёта из последней транзакции
                account.Balance = otherTransactions.Last().Balance;
                return;
            }

            // если нет последующих транзакций, то текущая транзакция стала последней
            // берём баланс из неё
            editingTransaction.UpdateBalance(initialFromBalance);
            account.Balance = editingTransaction.Balance;
            return;
        }

        { 
            // 9:45---10:00(Стало)---10:15---10:30(Было)--10:45
            // others >= 10:00(Стало)
            var otherTransactions = transactionsQuery
                .Where(x => x.CreatedAt >= editingTransaction.CreatedAt)
                .Where(x => !x.Id.Equals(editingTransaction.Id))
                .OrderBy(x => x.CreatedAt)
                .ToList();

            // получаем предыдущую транзакцию от текущей
            // 9:45
            var earlierTransaction = transactionsQuery.FirstOrDefault(x => x.CreatedAt < editingTransaction.CreatedAt);
            if (earlierTransaction == null)
            {
                // Если не найдена такая траназкция
                if (otherTransactions.Any())
                {
                    // Если есть другие транзакции, идущие после новой
                    // Начальный баланс берём из самой первой транзакции, которая идёт сразу после новой
                    // Т.е. мы как бы переносим этот начальный баланс в эту транзакцию
                    editingTransaction.UpdateBalance(otherTransactions.First().FromBalance());
                    UpdateBalanceForTransactions(otherTransactions, editingTransaction.Balance);
                    account.Balance = otherTransactions.Last().Balance;
                    return;
                }

                // Получается, что это самая первая транзакция, у неё же было изменено время
                // Нет ранней транзакции и нет поздних транзакций
                // Начальный баланс переносим из начального баланса существующей транзакции
                editingTransaction.UpdateBalance(initialFromBalance);
                account.Balance = editingTransaction.Balance;
                return;
            }

            // Если найдена такая транзакция
            editingTransaction.UpdateBalance(earlierTransaction.Balance);
            if (otherTransactions.Any())
            {
                // Обновляем балансы последующих транзакций
                // Начальный баланс берём из новой транзакции, которая "встанет между ними" по времени
                UpdateBalanceForTransactions(otherTransactions, editingTransaction.Balance);
                // Баланс для счёта берём из последней транзакции
                account.Balance = otherTransactions.Last().Balance;
                return;
            }

            // Последующих транзакций не найдено, баланс для счёта берём из новой транзакции
            account.Balance = editingTransaction.Balance;
        }
    }
}
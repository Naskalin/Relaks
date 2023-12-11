using Microsoft.EntityFrameworkCore;
using Relaks.Database;
using Relaks.Mappers;
using Relaks.Models.FinancialModels;
using Relaks.Views.Pages.Financials.ViewModels;

namespace Relaks.Managers;

public class FinancialManager(AppDbContext db)
{
    public void CreateTransaction(FinancialTransactionRequest req)
    {
        var transaction = new FinancialTransaction();
        req.MapTo(transaction);
        CreateItemsForTransaction(transaction, req);
        transaction.UpdateTotal();
        UpdateBalanceForNewTransaction(transaction);
        db.FinancialTransactions.Add(transaction);
    }

    public void UpdateTransaction(FinancialTransaction transaction, FinancialTransactionRequest req)
    {
        var initialFromBalance = transaction.FromBalance();
        var initialCreatedAt = transaction.CreatedAt;
        req.MapTo(transaction);
        DeleteItemsForTransaction(transaction, req);
        UpdateItemsForTransaction(transaction, req);
        CreateItemsForTransaction(transaction, req);
        transaction.UpdateTotal();
        UpdateBalanceForExistingTransaction(transaction, initialFromBalance, initialCreatedAt);
    }
    
    private static void CreateItemsForTransaction(FinancialTransaction transaction, FinancialTransactionRequest req)
    {
        var newItems = req
            .Items
            .Where(x => !x.Id.HasValue) // new items has no id
            .Select(x =>
            {
                var item = new FinancialTransactionItem();
                x.MapTo(item);
                return item;
            }).ToList();

        transaction.Items.AddRange(newItems);
    }

    private static void DeleteItemsForTransaction(FinancialTransaction transaction, FinancialTransactionRequest req)
    {
        var reqIds = req.Items.Where(x => x.Id.HasValue).Select(x => x.Id!.Value).ToList();
        transaction.Items.RemoveAll(x => !reqIds.Contains(x.Id));
    }

    private static void UpdateItemsForTransaction(FinancialTransaction transaction, FinancialTransactionRequest req)
    {
        req.Items.Where(x => x.Id.HasValue).ToList().ForEach(reqItem =>
        {
            var item = transaction.Items.First(t => t.Id.Equals(reqItem.Id));
            reqItem.MapTo(item);
        });
    }

    private FinancialTransaction? PreviousTransaction { get; set; }

    /// <summary>
    /// Отсортированные транзакции
    /// </summary>
    /// <param name="orderedTransactions">Отсортированные транзакции в порядке возрастания</param>
    /// <param name="fromBalance">Начальный баланс для первой транзакции</param>
    private void UpdateBalanceForTransactions(List<FinancialTransaction> orderedTransactions, decimal fromBalance)
    {
        PreviousTransaction = null;

        foreach (var transaction in orderedTransactions)
        {
            transaction.UpdateBalance(PreviousTransaction?.Balance ?? fromBalance);
            PreviousTransaction = transaction;
        }
    }

    private void UpdateBalanceForNewTransaction(FinancialTransaction newTransaction)
    {
        var account = db.FinancialAccounts.First(x => x.Id.Equals(newTransaction.AccountId));
        var transactionsQuery = db
                .FinancialTransactions
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

    /// <summary>
    /// Обновляем все балансы в соответствии с новой транзакцией
    /// </summary>
    /// <param name="editingTransaction">Транзакция, до её сохранения в базе данных</param>
    /// <param name="initialFromBalance">Исходный FromBalance(), до изменения модели</param>
    /// <param name="initialCreatedAt">Исходная дата, до изменения модели</param>
    private void UpdateBalanceForExistingTransaction(FinancialTransaction editingTransaction, decimal initialFromBalance, DateTime initialCreatedAt)
    {
        var account = db.FinancialAccounts.First(x => x.Id.Equals(editingTransaction.AccountId));
        var transactionsQuery = db
                .FinancialTransactions
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
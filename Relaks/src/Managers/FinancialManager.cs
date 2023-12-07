using Microsoft.EntityFrameworkCore;
using Relaks.Database;
using Relaks.Mappers;
using Relaks.Models.FinancialModels;
using Relaks.Views.Pages.Financials.ViewModels;

namespace Relaks.Managers;

public class FinancialManager(AppDbContext db)
{
    public static void CreateItemsForTransaction(FinancialTransaction transaction, FinancialTransactionRequest req)
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
    
    public void DeleteItemsForTransaction(FinancialTransaction transaction, FinancialTransactionRequest req)
    {
        var reqIds = req.Items.Where(x => x.Id.HasValue).Select(x => x.Id!.Value).ToList();
        transaction.Items.RemoveAll(x => !reqIds.Contains(x.Id));
    }
    
    public static void UpdateItemsForTransaction(FinancialTransaction transaction, FinancialTransactionRequest req)
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

    public void UpdateBalanceForNewTransaction(FinancialTransaction newTransaction)
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
            // 9:45---10:00(Новая)---10:15---10:30(Последняя)
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
                    newTransaction.UpdateBalance(otherTransactions.First().Balance);
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
    public void UpdateBalanceForExistingTransaction(FinancialTransaction editingTransaction, decimal initialFromBalance, DateTime initialCreatedAt)
    {
        var account = db.FinancialAccounts.First(x => x.Id.Equals(editingTransaction.AccountId));
        var transactionsQuery = db
                .FinancialTransactions
                .Include(x => x.Items)
                .OrderByDescending(x => x.CreatedAt)
                .Where(x => x.AccountId.Equals(editingTransaction.AccountId))
            ;

        {
            if (initialCreatedAt.Equals(editingTransaction.CreatedAt))
            {
                // Время транзакции не изменено
                // Транзакция есть в бд, это изменение существующей транзакции
                // Получаем все последующие транзакции после существующей, исключая её саму
                var otherTransactions = transactionsQuery
                    .Where(x => x.CreatedAt >= initialCreatedAt)
                    .Where(x => !x.Id.Equals(editingTransaction.Id))
                    .OrderBy(x => x.CreatedAt)
                    .ToList();
    
                editingTransaction.UpdateBalance(initialFromBalance);
                if (otherTransactions.Any())
                {
                    // Если есть другие транзакции, идущие после текущей, то обновляем их балансы
                    UpdateBalanceForTransactions(otherTransactions, editingTransaction.Balance);
                    account.Balance = otherTransactions.Last().Balance;
                    return;
                }
    
                // Если нет других транзакций, идущих после текущей, то просто обновляем баланс на счёте
                // Получается это изменение единственной транзакции
                account.Balance = editingTransaction.Balance;
                return;
            }
            
            // Время транзакции было изменено
            // 9:45---10:00(Было)---10:15---10:30(Стало)--10:45
            if (editingTransaction.CreatedAt > initialCreatedAt)
            {
                // Промежуточные транзакции
                // 10:00(Было) <= others && others < 10:30(Стало)
                var betweenTransactions = transactionsQuery
                    .Where(x => initialCreatedAt <= x.CreatedAt && x.CreatedAt < editingTransaction.CreatedAt)
                    .Where(x => !x.Id.Equals(editingTransaction.Id))
                    .OrderBy(x => x.CreatedAt)
                    .ToList();
                if (betweenTransactions.Any())
                {
                    // Если есть, обновляем их, начальный баланс берём из существующей в бд транзакции
                    UpdateBalanceForTransactions(betweenTransactions, initialFromBalance);
                    // обновляем баланс текущей транзакции, начальный баланс берём из последней обновлённой транзакции
                    editingTransaction.UpdateBalance(betweenTransactions.Last().Balance);
                }
                else
                {
                    // Если нет, то обновляем баланс текущей по её же начальному балансу из бд
                    editingTransaction.UpdateBalance(initialFromBalance);
                }
                
                // Последующие транзакции
                // others >= 10:30(Стало)
                var otherTransactions = transactionsQuery
                    .Where(x => x.CreatedAt >= editingTransaction.CreatedAt)
                    .Where(x => !x.Id.Equals(editingTransaction.Id))
                    .OrderBy(x => x.CreatedAt)
                    .ToList();
                if (otherTransactions.Any())
                {
                    // если есть последующие, то обновляем их балансы
                    UpdateBalanceForTransactions(otherTransactions, editingTransaction.Balance);
                    // и задаём баланс счёта из последней транзакции
                    account.Balance = otherTransactions.Last().Balance;
                    return;
                }
    
                // если нет последующих транзакций, то текущая транзакция стала последней
                // берём баланс из неё
                account.Balance = editingTransaction.Balance;
            }
            else
            {
                // 9:45---10:00(Стало)---10:15---10:30(Было)--10:45
                // others >= 10:00(Стало)
                var otherTransactions = transactionsQuery
                    .Where(x => x.CreatedAt >= editingTransaction.CreatedAt)
                    .Where(x => !x.Id.Equals(editingTransaction.Id))
                    .OrderBy(x => x.CreatedAt)
                    .ToList();
                
                // получаем предыдущую транзакцию от текущей
                var earlierTransaction = transactionsQuery.FirstOrDefault(x => x.CreatedAt < editingTransaction.CreatedAt);
                if (earlierTransaction == null)
                {
                    // Если не найдена такая траназкция
                    if (otherTransactions.Any())
                    {
                        // Если есть другие транзакции, идущие после новой
                        // Начальный баланс берём из самой первой транзакции, которая идёт сразу после новой
                        // Т.е. мы как бы переносим этот начальный баланс в эту транзакцию
                        editingTransaction.UpdateBalance(otherTransactions.First().Balance);
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
}
using Relaks.Database;
using Relaks.Mappers;
using Relaks.Models.FinancialModels;
using Relaks.Views.Pages.Financials.ViewModels;

namespace Relaks.Managers;

public class FinancialManager(AppDbContext db)
{
    public void CreateItemsForTransaction(FinancialTransaction transaction, FinancialTransactionRequest req)
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
        transaction.Items.ForEach(x =>
        {
            if (!reqIds.Contains(x.Id))
            {
                transaction.Items.Remove(x);
            }
        });
    }
    
    public void UpdateItemsForTransaction(FinancialTransaction transaction, FinancialTransactionRequest req)
    {
        req.Items.Where(x => x.Id.HasValue).ToList().ForEach(x =>
        {
            var item = transaction.Items.First(i => i.Id.Equals(x.Id));
            x.MapTo(item);
        });
    }
    
    private FinancialTransaction? PreviousTransaction { get; set; }
    
    /// <summary>
    /// Отсортированные транзакции
    /// </summary>
    /// <param name="orderedTransactions"></param>
    /// <param name="fromBalance"></param>
    private void UpdateBalanceForTransactions(List<FinancialTransaction> orderedTransactions, decimal fromBalance)
    {
        PreviousTransaction = null;
        
        foreach (var transaction in orderedTransactions)
        {
            transaction.UpdateBalance(PreviousTransaction?.Balance ?? fromBalance);
            PreviousTransaction = transaction;
        }
    }
    
    /// <summary>
    /// Обновляем все балансы в соответствии с новой транзакцией
    /// </summary>
    /// <param name="newTransaction">Транзакция, до её сохранения в базе данных</param>
    public void UpdateBalanceForTransaction(FinancialTransaction newTransaction)
    {
        var account = db.FinancialAccounts.First(x => x.Id.Equals(newTransaction.AccountId));
        var transactionsQuery = db
            .FinancialTransactions
            .OrderByDescending(x => x.CreatedAt)
            .Where(x => x.AccountId.Equals(newTransaction.AccountId))
            ;
        
        // последняя транзакция
        var lastTransaction = transactionsQuery.FirstOrDefault();
        if (lastTransaction == null)
        {
            // Транзакций не было, это первая
            // Нужно обновить баланс только этой транзакции и аккаунта
            // Начальный баланс берём из счёта
            newTransaction.UpdateBalance(account.Balance);
            account.Balance = newTransaction.Balance;
            return;
        }

        var existTransaction = transactionsQuery.FirstOrDefault(x => x.Id.Equals(newTransaction.Id));
        if (existTransaction == null)
        {
            // Транзакции нет в бд, это новая транзакция
            
            if (newTransaction.CreatedAt > lastTransaction.CreatedAt)
            {
                // Новая транзакция станет самой последней
                // Нужно обновить баланс только этой транзакции и аккаунта
                // Начальный баланс в берём из последней транзакции
                newTransaction.UpdateBalance(lastTransaction.Balance);
                account.Balance = newTransaction.Balance;
                return;
            }
            
            // Новая транзакция добавиться до последней
            // Нужно обновить баланс этой и всех последующих транзакций и аккаунта
            // Начальный баланс берём из первой транзакции, которая идёт до этой
            // 9:45---10:00(Новая)---10:15---10:30(Последняя)
            // 9:45 < 10:00 - самая свежая
            // others >= 10:00
            var earlierTransaction = transactionsQuery.FirstOrDefault(x => x.CreatedAt < newTransaction.CreatedAt);
            
            // Остальные транзакции, пойдут после текущей в порядке возрастания по дате
            var othersTransactions = transactionsQuery
                .Where(x => x.CreatedAt >= newTransaction.CreatedAt)
                .OrderBy(x => x.CreatedAt)
                .ToList();
            if (earlierTransaction == null)
            {
                // Если не найдена такая траназкция
                if (othersTransactions.Any())
                {
                    // Если есть другие транзакции, идущие после новой
                    // Начальный баланс берём из самой первой транзакции, которая идёт сразу после новой
                    // Т.е. мы как бы переносим этот начальный баланс в эту транзакцию
                    newTransaction.UpdateBalance(othersTransactions.First().Balance);
                    UpdateBalanceForTransactions(othersTransactions, newTransaction.Balance);
                    account.Balance = othersTransactions.Last().Balance;
                    return;
                }
                
                // Ситуация не должна произойти, её должен обработать код выше
                // Получается, что это самая первая транзакция
                // Нет ранней транзакции и нет поздних транзакций
                // Обработаем на всякий случай как и с самой первой транзакцией
                newTransaction.UpdateBalance(account.Balance);
                account.Balance = newTransaction.Balance;
                return;
            }
            
            // Если найдена такая транзакция
            newTransaction.UpdateBalance(earlierTransaction.Balance);
            if (othersTransactions.Any())
            {
                // Обновляем балансы последующих транзакций
                // Начальный баланс берём из новой транзакции, которая "встанет между ними" по времени
                UpdateBalanceForTransactions(othersTransactions, newTransaction.Balance);
                // Баланс для счёта берём из последней транзакции
                account.Balance = othersTransactions.Last().Balance;
                return;
            }
            
            // Последующих транзакций не найдено, баланс для счёта берём из новой транзакции
            account.Balance = newTransaction.Balance;
            return;
        }
        
        {
            // Транзакция есть в бд, это изменение существующей транзакции
            // Получаем все последующие транзакции после существующей, исключая её саму
            var othersTransactions = transactionsQuery
                .Where(x => x.CreatedAt >= existTransaction.CreatedAt)
                .Where(x => x.Id.Equals(existTransaction.Id))
                .OrderBy(x => x.CreatedAt)
                .ToList();

            newTransaction.UpdateBalance(existTransaction.FromBalance());
            if (othersTransactions.Any())
            {
                // Если есть другие транзакции, идущие после текущей, то обновляем их балансы
                UpdateBalanceForTransactions(othersTransactions, newTransaction.Balance);
                account.Balance = othersTransactions.Last().Balance;
                return;
            }

            // Если нет других транзакций, идущих после текущей, то просто обновляем баланс на счёте
            // Получается это изменение единственной транзакции
            account.Balance = newTransaction.Balance;
        }
    }
}
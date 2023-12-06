using Relaks.Database;
using Relaks.Mappers;
using Relaks.Models.FinancialModels;
using Relaks.Views.Pages.Financials.ViewModels;

namespace Relaks.Managers;

public class FinancialManager
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
    
    public static void DeleteItemsForTransaction(FinancialTransaction transaction, FinancialTransactionRequest req)
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
    
    public static void UpdateItemsForTransaction(FinancialTransaction transaction, FinancialTransactionRequest req)
    {
        req.Items.Where(x => x.Id.HasValue).ToList().ForEach(x =>
        {
            var item = transaction.Items.First(i => i.Id.Equals(x.Id));
            x.MapTo(item);
        });
    }
    
    private FinancialTransaction? PreviousTransaction { get; set; }
    
    // /// <summary>
    // /// Отсортированные транзакции
    // /// </summary>
    // /// <param name="orderedTransactions">Отсортированные транзакции в порядке возрастания</param>
    // /// <param name="fromBalance">Начальный баланс для первой транзакции</param>
    // private void UpdateBalanceForTransactions(List<FinancialTransaction> orderedTransactions, decimal fromBalance)
    // {
    //     PreviousTransaction = null;
    //     
    //     foreach (var transaction in orderedTransactions)
    //     {
    //         transaction.UpdateBalance(PreviousTransaction?.Balance ?? fromBalance);
    //         PreviousTransaction = transaction;
    //     }
    // }
    
    // /// <summary>
    // /// Обновляем все балансы в соответствии с новой транзакцией
    // /// </summary>
    // /// <param name="newTransaction">Транзакция, до её сохранения в базе данных</param>
    // public void UpdateBalanceForTransaction(FinancialTransaction newTransaction)
    // {
    //     var account = db.FinancialAccounts.First(x => x.Id.Equals(newTransaction.AccountId));
    //     var transactionsQuery = db
    //         .FinancialTransactions
    //         .OrderByDescending(x => x.CreatedAt)
    //         .Where(x => x.AccountId.Equals(newTransaction.AccountId))
    //         ;
    //     
    //     // последняя транзакция
    //     var lastTransaction = transactionsQuery.FirstOrDefault();
    //     if (lastTransaction == null)
    //     {
    //         // Транзакций не было, это первая
    //         // Нужно обновить баланс только этой транзакции и аккаунта
    //         // Начальный баланс берём из счёта
    //         newTransaction.UpdateBalance(account.Balance);
    //         account.Balance = newTransaction.Balance;
    //         return;
    //     }
    //
    //     var existTransaction = transactionsQuery.FirstOrDefault(x => x.Id.Equals(newTransaction.Id));
    //     if (existTransaction == null)
    //     {
    //         // Транзакции нет в бд, это новая транзакция
    //         
    //         if (newTransaction.CreatedAt > lastTransaction.CreatedAt)
    //         {
    //             // Новая транзакция станет самой последней
    //             // Нужно обновить баланс только этой транзакции и аккаунта
    //             // Начальный баланс в берём из последней транзакции
    //             newTransaction.UpdateBalance(lastTransaction.Balance);
    //             account.Balance = newTransaction.Balance;
    //             return;
    //         }
    //         
    //         // Новая транзакция добавиться до последней
    //         // Нужно обновить баланс этой и всех последующих транзакций и аккаунта
    //         // Начальный баланс берём из первой транзакции, которая идёт до этой
    //         // 9:45---10:00(Новая)---10:15---10:30(Последняя)
    //         // 9:45 < 10:00 - самая свежая
    //         // others >= 10:00
    //         var earlierTransaction = transactionsQuery.FirstOrDefault(x => x.CreatedAt < newTransaction.CreatedAt);
    //         
    //         // Остальные транзакции, пойдут после текущей в порядке возрастания по дате
    //         var otherTransactions = transactionsQuery
    //             .Where(x => x.CreatedAt >= newTransaction.CreatedAt)
    //             .OrderBy(x => x.CreatedAt)
    //             .ToList();
    //         if (earlierTransaction == null)
    //         {
    //             // Если не найдена такая траназкция
    //             if (otherTransactions.Any())
    //             {
    //                 // Если есть другие транзакции, идущие после новой
    //                 // Начальный баланс берём из самой первой транзакции, которая идёт сразу после новой
    //                 // Т.е. мы как бы переносим этот начальный баланс в эту транзакцию
    //                 newTransaction.UpdateBalance(otherTransactions.First().Balance);
    //                 UpdateBalanceForTransactions(otherTransactions, newTransaction.Balance);
    //                 account.Balance = otherTransactions.Last().Balance;
    //                 return;
    //             }
    //             
    //             // Ситуация не должна произойти, её должен обработать код выше
    //             // Получается, что это самая первая транзакция
    //             // Нет ранней транзакции и нет поздних транзакций
    //             // Обработаем на всякий случай как и с самой первой транзакцией
    //             newTransaction.UpdateBalance(account.Balance);
    //             account.Balance = newTransaction.Balance;
    //             return;
    //         }
    //         
    //         // Если найдена такая транзакция
    //         newTransaction.UpdateBalance(earlierTransaction.Balance);
    //         if (otherTransactions.Any())
    //         {
    //             // Обновляем балансы последующих транзакций
    //             // Начальный баланс берём из новой транзакции, которая "встанет между ними" по времени
    //             UpdateBalanceForTransactions(otherTransactions, newTransaction.Balance);
    //             // Баланс для счёта берём из последней транзакции
    //             account.Balance = otherTransactions.Last().Balance;
    //             return;
    //         }
    //         
    //         // Последующих транзакций не найдено, баланс для счёта берём из новой транзакции
    //         account.Balance = newTransaction.Balance;
    //         return;
    //     }
    //     
    //     {
    //         if (existTransaction.CreatedAt.Equals(newTransaction.CreatedAt))
    //         {
    //             // Время транзакции не изменено
    //             // Транзакция есть в бд, это изменение существующей транзакции
    //             // Получаем все последующие транзакции после существующей, исключая её саму
    //             var otherTransactions = transactionsQuery
    //                 .Where(x => x.CreatedAt >= existTransaction.CreatedAt)
    //                 .Where(x => !x.Id.Equals(existTransaction.Id))
    //                 .OrderBy(x => x.CreatedAt)
    //                 .ToList();
    //
    //             newTransaction.UpdateBalance(existTransaction.FromBalance());
    //             if (otherTransactions.Any())
    //             {
    //                 // Если есть другие транзакции, идущие после текущей, то обновляем их балансы
    //                 UpdateBalanceForTransactions(otherTransactions, newTransaction.Balance);
    //                 account.Balance = otherTransactions.Last().Balance;
    //                 return;
    //             }
    //
    //             // Если нет других транзакций, идущих после текущей, то просто обновляем баланс на счёте
    //             // Получается это изменение единственной транзакции
    //             account.Balance = newTransaction.Balance;
    //             return;
    //         }
    //         
    //         // Время транзакции было изменено
    //         // 9:45---10:00(Было)---10:15---10:30(Стало)--10:45
    //         if (newTransaction.CreatedAt > existTransaction.CreatedAt)
    //         {
    //             // Промежуточные транзакции
    //             // 10:00(Было) <= others && others < 10:30(Стало)
    //             var betweenTransactions = transactionsQuery
    //                 .Where(x => existTransaction.CreatedAt <= x.CreatedAt && x.CreatedAt < newTransaction.CreatedAt)
    //                 .Where(x => !x.Id.Equals(existTransaction.Id))
    //                 .OrderBy(x => x.CreatedAt)
    //                 .ToList();
    //             if (betweenTransactions.Any())
    //             {
    //                 // Если есть, обновляем их, начальный баланс берём из существующей в бд транзакции
    //                 UpdateBalanceForTransactions(betweenTransactions, existTransaction.FromBalance());
    //                 // обновляем баланс текущей транзакции, начальный баланс берём из последней обновлённой транзакции
    //                 newTransaction.UpdateBalance(betweenTransactions.Last().Balance);
    //             }
    //             else
    //             {
    //                 // Если нет, то обновляем баланс текущей по её же начальному балансу из бд
    //                 newTransaction.UpdateBalance(existTransaction.FromBalance());
    //             }
    //             
    //             // Последующие транзакции
    //             // others >= 10:30(Стало)
    //             var otherTransactions = transactionsQuery
    //                 .Where(x => x.CreatedAt >= newTransaction.CreatedAt)
    //                 .Where(x => !x.Id.Equals(existTransaction.Id))
    //                 .OrderBy(x => x.CreatedAt)
    //                 .ToList();
    //             if (otherTransactions.Any())
    //             {
    //                 // если есть последующие, то обновляем их балансы
    //                 UpdateBalanceForTransactions(otherTransactions, newTransaction.Balance);
    //                 // и задаём баланс счёта из последней транзакции
    //                 account.Balance = otherTransactions.Last().Balance;
    //                 return;
    //             }
    //
    //             // если нет последующих транзакций, то текущая транзакция стала последней
    //             // берём баланс из неё
    //             account.Balance = newTransaction.Balance;
    //         }
    //         else
    //         {
    //             // 9:45---10:00(Стало)---10:15---10:30(Было)--10:45
    //             // others >= 10:00(Стало)
    //             var otherTransactions = transactionsQuery
    //                 .Where(x => x.CreatedAt >= newTransaction.CreatedAt)
    //                 .Where(x => !x.Id.Equals(existTransaction.Id))
    //                 .OrderBy(x => x.CreatedAt)
    //                 .ToList();
    //             
    //             // получаем предыдущую транзакцию от текущей
    //             var earlierTransaction = transactionsQuery.FirstOrDefault(x => x.CreatedAt < newTransaction.CreatedAt);
    //             if (earlierTransaction == null)
    //             {
    //                 // Если не найдена такая траназкция
    //                 if (otherTransactions.Any())
    //                 {
    //                     // Если есть другие транзакции, идущие после новой
    //                     // Начальный баланс берём из самой первой транзакции, которая идёт сразу после новой
    //                     // Т.е. мы как бы переносим этот начальный баланс в эту транзакцию
    //                     newTransaction.UpdateBalance(otherTransactions.First().Balance);
    //                     UpdateBalanceForTransactions(otherTransactions, newTransaction.Balance);
    //                     account.Balance = otherTransactions.Last().Balance;
    //                     return;
    //                 }
    //                 
    //                 // Получается, что это самая первая транзакция, у неё же было изменено время
    //                 // Нет ранней транзакции и нет поздних транзакций
    //                 // Начальный баланс переносим из начального баланса существующей транзакции
    //                 newTransaction.UpdateBalance(existTransaction.FromBalance());
    //                 account.Balance = newTransaction.Balance;
    //                 return;
    //             }
    //             
    //             // Если найдена такая транзакция
    //             newTransaction.UpdateBalance(earlierTransaction.Balance);
    //             if (otherTransactions.Any())
    //             {
    //                 // Обновляем балансы последующих транзакций
    //                 // Начальный баланс берём из новой транзакции, которая "встанет между ними" по времени
    //                 UpdateBalanceForTransactions(otherTransactions, newTransaction.Balance);
    //                 // Баланс для счёта берём из последней транзакции
    //                 account.Balance = otherTransactions.Last().Balance;
    //                 return;
    //             }
    //             
    //             // Последующих транзакций не найдено, баланс для счёта берём из новой транзакции
    //             account.Balance = newTransaction.Balance;
    //         }
    //     }
    // }
}
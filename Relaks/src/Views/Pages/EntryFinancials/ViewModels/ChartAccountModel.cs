using Relaks.Models.FinancialModels;

namespace Relaks.Views.Pages.EntryFinancials.ViewModels;

public class ChartLineModel
{
    public string? CurrencySymbol { get; set; }
    
    /// <summary>
    /// 3 symbol Currency, like RUB, USD, EUR
    /// </summary>
    public string? CurrencyId { get; set; }
    public List<DateTime> Dates { get; set; } = new(); 
    
    /// <summary>
    /// Изменение баланса за этот период
    /// </summary>
    public decimal BalanceChanges { get; set; }
    
    /// <summary>
    /// Пополнений за этот период
    /// </summary>
    public decimal TotalIncome { get; set; }
    
    /// <summary>
    /// Списаний за этот период
    /// </summary>
    public decimal TotalOutlay { get; set; }
    
    /// <summary>
    /// Средний баланс за этот период
    /// </summary>
    public decimal AverageBalance { get; set; }
    
    /// <summary>
    /// Кол-во транзакций
    /// </summary>
    public int PlusTransactionsCount { get; set; }
    
    public int MinusTransactionsCount { get; set; }

    /// <summary>
    /// Название
    /// </summary>
    public string Title { get; set; } = "???";

    public List<ChartItemModel> Items { get; set; } = new();
    public List<ChartCategoryItem> CategoryItems { get; set; } = new();
    
}

public class ChartCategoryItem
{
    /// <summary>
    /// Идентификатор категории
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Название категории
    /// </summary>
    /// 
    public string Title { get; set; } = "";
    
    /// <summary>
    /// Количество операций в этой категории
    /// </summary>
    public int Count { get; set; }
    
    /// <summary>
    /// Сумма пополнений
    /// </summary>
    public decimal TotalIncome { get; set; }
    
    /// <summary>
    /// Сумма пополнений
    /// </summary>
    public decimal TotalOutcome { get; set; }
    
    /// <summary>
    /// Процент использвания
    /// </summary>
    public double Percent { get; set; }

    public string? TreePath { get; set; }
    
    /// <summary>
    /// Категория не использовалась напрямую, была чьим то родителем
    /// </summary>
    public bool IsNotUsedDirectly  { get; set; }
}

public class ChartItemModel
{
    public DateTime Date { get; set; }
    
    /// <summary>
    /// Баланс в последний момент даты
    /// Например для дня, - это баланс последней транзакции этого дня
    /// Для месяца, - баланс последнего дня последней транзакции
    /// </summary>
    public decimal EndOfDateBalance { get; set; }
    
    public decimal BalanceChanges { get; set; }
    /// <summary>
    /// Пополнений в эту дату
    /// </summary>
    public decimal TotalIncome { get; set; }
    
    /// <summary>
    /// Списаний в эту дату
    /// </summary>
    public decimal TotalOutlay { get; set; }
    
    /// <summary>
    /// Средний баланс в эту дату
    /// </summary>
    public decimal AverageBalance { get; set; }

    /// <summary>
    /// Использование категорий
    /// category id, category usage percent
    /// </summary>
    public Dictionary<Guid, double> CategoryUsage { get; set; } = new();
}
namespace Relaks.Views.Shared.Charts.ViewModels;

public class FinancialAccountLineChartModel
{
    public string? CurrencySymbol { get; set; }
    
    /// <summary>
    /// 3 symbol Currency, like RUB, USD, EUR
    /// </summary>
    public string? CurrencyId { get; set; }
    public List<DateTime> Dates { get; set; } = new(); 
    public List<FinancialAccountChartModel> Accounts { get; set; } = new();
}

public class FinancialAccountChartModel
{
    
    public decimal Total { get; set; }
    
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
    public int TransactionsCount { get; set; }

    /// <summary>
    /// Название
    /// </summary>
    public string Title { get; set; } = "???";

    public List<FinancialAccountChartItemModel> Items { get; set; } = new();
}

public class FinancialAccountChartItemModel
{
    public DateTime Date { get; set; }
    public decimal Total { get; set; }
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
}
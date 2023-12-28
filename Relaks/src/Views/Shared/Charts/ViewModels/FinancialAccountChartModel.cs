namespace Relaks.Views.Shared.Charts.ViewModels;

public class FinancialAccountLineChartModel
{
    public enum TypeEnum
    {
        Transactions,
        Days,
        Months,
    }

    public TypeEnum Type { get; set; } = TypeEnum.Days;
    
    /// <summary>
    /// Период от
    /// </summary>
    public DateTime From { get; set; }
    
    /// <summary>
    /// Период до
    /// </summary>
    public DateTime To { get; set; }
    
    public string HtmlElementId { get; set; } = null!;
    public List<FinancialAccountChartModel> Accounts { get; set; } = new();
}

public class FinancialAccountChartModel
{
    
    public decimal Total { get; set; }
    
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
    public decimal AverageBalance { get; set; }
}
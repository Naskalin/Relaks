using Microsoft.EntityFrameworkCore;

namespace Relaks.Models.FinancialModels;

public class FinancialAccount
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string? Description { get; set; }

    public string FinancialCurrencyId { get; set; } = null!;
    public FinancialCurrency FinancialCurrency { get; set; } = null!;

    public Guid CategoryId { get; set; }
    public FinancialAccountCategory Category { get; set; } = null!;

    /// <summary>
    /// Дата открытия счёта
    /// </summary>
    public DateTime StartAt { get; set; }
    
    /// <summary>
    /// Дата закрытия счёта
    /// </summary>
    public DateTime? EndAt { get; set; }

    /// <summary>
    /// Владелец счёта
    /// </summary>
    public Guid EntryId { get; set; }
    public BaseEntry Entry { get; set; } = null!;

    public string TitleWithCurrency() => $"{Title} ({FinancialCurrencyId}), {Balance.ToString("N2")} {FinancialCurrency.Symbol}";
    
    /// <summary>
    /// Текущий баланс
    /// А так же используется как начальный баланс, задаётся при создании счёта
    /// </summary>
    [Precision(19, 4)]
    public decimal Balance { get; set; }
}
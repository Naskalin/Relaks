using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Relaks.Models.FinancialModels;

public class FinancialAccount
{
    public Guid Id { get; set; }
    [MaxLength(255)]
    public string Title { get; set; } = null!;
    
    [MaxLength(500)]
    public string? Description { get; set; }

    [MaxLength(3)]
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

    // /// <summary>
    // /// Владелец счёта
    // /// </summary>
    // public Guid EntryId { get; set; }
    // public BaseEntry Entry { get; set; } = null!;

    public string TitleWithCurrency() => $"{Title} ({FinancialCurrency.Symbol})";
    public string TitleWithCurrencyAndBalance() => $"{Title} ({FinancialCurrencyId}), {Balance.ToString("N2")} {FinancialCurrency.Symbol}";
    
    /// <summary>
    /// Текущий баланс
    /// А так же используется как начальный баланс, задаётся при создании счёта
    /// </summary>
    [Precision(19, 4)]
    public decimal Balance { get; set; }

    public List<BaseFinancialTransaction> Transactions { get; set; } = new();
}
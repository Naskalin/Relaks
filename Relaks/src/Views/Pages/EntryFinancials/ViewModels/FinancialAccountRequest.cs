namespace Relaks.Views.Pages.EntryFinancials.ViewModels;

public class FinancialAccountRequest
{
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public string FinancialCurrencyId { get; set; } = null!;
    public Guid CategoryId { get; set; }
    public DateTime StartAt { get; set; } = DateTime.Now;
    public DateTime? EndAt { get; set; }
    public decimal? InitialBalance { get; set; }
}
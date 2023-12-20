using System.ComponentModel.DataAnnotations;

namespace Relaks.Models.FinancialModels;

public class FinancialAccountCategory
{
    public Guid Id { get; set; }
    [MaxLength(255)]
    public string Title { get; set; } = null!;
    public List<FinancialAccount> Accounts { get; set; } = new();
    public Guid EntryId { get; set; }
    public BaseEntry Entry { get; set; } = null!;
}
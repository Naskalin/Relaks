namespace Relaks.Models.FinancialModels;

public class FinancialAccountCategory
{
    public Guid Id { get; } = Guid.NewGuid();
    public required string Title { get; set; }
}
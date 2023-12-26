using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Relaks.Models.FinancialModels;

namespace Relaks.Database.Configurations;

public class FinancialTransactionConfiguration : IEntityTypeConfiguration<BaseFinancialTransaction>
{
    public void Configure(EntityTypeBuilder<BaseFinancialTransaction> builder)
    {
        builder.HasMany(t => t.Items)
            .WithOne(i => i.Transaction)
            .HasForeignKey(i => i.TransactionId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

public class FinancialAccountConfiguration : IEntityTypeConfiguration<FinancialAccount>
{
    public void Configure(EntityTypeBuilder<FinancialAccount> builder)
    {
        builder.HasMany(a => a.Transactions)
            .WithOne(i => i.Account)
            .HasForeignKey(i => i.AccountId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
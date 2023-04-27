using Relaks.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Relaks.Database.Configurations;

public class FtsEntryConfiguration : IEntityTypeConfiguration<FtsEntry>
{
    public void Configure(EntityTypeBuilder<FtsEntry> builder)
    {
        builder.HasKey(fts => fts.RowId);
        builder.ToTable(FtsTableNames.FtsEntries, x => x.ExcludeFromMigrations());
        builder.Property(fts => fts.Match).HasColumnName(FtsTableNames.FtsEntries);
    }
}

public class FtsEntryInfoConfiguration : IEntityTypeConfiguration<FtsEntryInfo>
{
    public void Configure(EntityTypeBuilder<FtsEntryInfo> builder)
    {
        builder.HasKey(fts => fts.RowId);
        builder.ToTable(FtsTableNames.FtsEntryInfos, x => x.ExcludeFromMigrations());
        builder.Property(fts => fts.Match).HasColumnName(FtsTableNames.FtsEntryInfos);
    }
}
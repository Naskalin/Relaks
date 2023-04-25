using Relaks.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Relaks.Database.Configurations;

// public class EntryConfiguration : IEntityTypeConfiguration<BaseEntry>
// {
//     public void Configure(EntityTypeBuilder<BaseEntry> builder)
//     {
//         builder
//             .ToTable("Entries");
//             // .Property(x => x.Type)
//             // .HasConversion(new EnumToStringConverter<BaseEntry.TypeEnum>());
//     }
// }

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
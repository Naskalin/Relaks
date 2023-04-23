using Relaks.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Relaks.Database.Configurations;

public class EntryConfiguration : IEntityTypeConfiguration<Entry>
{
    public void Configure(EntityTypeBuilder<Entry> builder)
    {
        builder
            .Property(x => x.Type)
            .HasConversion(new EnumToStringConverter<Entry.TypeEnum>());
    }
}

// public class FtsEntryConfiguration : IEntityTypeConfiguration<FtsEntry>
// {
//     public void Configure(EntityTypeBuilder<FtsEntry> builder)
//     {
//         builder.HasKey(fts => fts.RowId);
//
//         builder.ToTable("FtsEntries", x => x.ExcludeFromMigrations());
//         
//         builder
//             .Property(fts => fts.Match)
//             .HasColumnName("FtsEntries");
//     }
// }
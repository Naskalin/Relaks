using Relaks.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Relaks.Database.Configurations;

public class EntryInfoConfiguration : IEntityTypeConfiguration<EntryInfo>
{
    public void Configure(EntityTypeBuilder<EntryInfo> builder)
    {
        builder.ToTable("EntryInfos");
    }
}

// public class FtsEntryInfoConfiguration : IEntityTypeConfiguration<FtsEntryInfo>
// {
//     public void Configure(EntityTypeBuilder<FtsEntryInfo> builder)
//     {
//         builder.HasKey(fts => fts.RowId);
//         
//         builder.ToTable("FtsEntryInfos", x => x.ExcludeFromMigrations());
//
//         builder
//             .Property(fts => fts.Match)
//             .HasColumnName("FtsEntryInfos");
//     }
// }
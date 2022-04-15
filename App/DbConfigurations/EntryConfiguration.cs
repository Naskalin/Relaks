using App.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace App.DbConfigurations;

public class EntryConfiguration : IEntityTypeConfiguration<Entry>
{
    public void Configure(EntityTypeBuilder<Entry> builder)
    {
        builder
            .Property(x => x.EntryType)
            .HasConversion(new EnumToStringConverter<EntryTypeEnum>());
    }
}

// public class EntryFtsConfiguration : IEntityTypeConfiguration<EntryFts>
// {
//     public void Configure(EntityTypeBuilder<EntryFts> builder)
//     {
//         builder.HasKey(fts => fts.RowId);
//         
//         builder
//             .Property(fts => fts.Match)
//             .HasColumnName(nameof(EntryFts));
//     }
// }
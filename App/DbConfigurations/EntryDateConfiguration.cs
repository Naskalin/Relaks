using App.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace App.DbConfigurations;

public class EntryDateConfiguration : IEntityTypeConfiguration<EntryDate>
{
    public void Configure(EntityTypeBuilder<EntryDate> builder)
    {
        builder.Property(x => x.DateType).HasConversion(new EnumToStringConverter<EntryDateTypeEnum>());
        builder.HasOne(x => x.Entry).WithMany(x => x.Dates);
    }
}
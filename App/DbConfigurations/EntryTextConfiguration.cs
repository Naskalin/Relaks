using App.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace App.DbConfigurations;

public class EntryTextConfiguration : IEntityTypeConfiguration<EntryText>
{
    public void Configure(EntityTypeBuilder<EntryText> builder)
    {
        builder.Property(x => x.TextType).HasConversion(new EnumToStringConverter<TextTypeEnum>());
        builder.HasOne(x => x.Entry).WithMany(x => x.Texts).HasForeignKey(x => x.EntryId);
    }
}
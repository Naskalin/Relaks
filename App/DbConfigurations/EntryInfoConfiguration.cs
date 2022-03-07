using App.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace App.DbConfigurations;

public class EntryInfoConfiguration : IEntityTypeConfiguration<EntryInfo>
{
    public void Configure(EntityTypeBuilder<EntryInfo> builder)
    {
        builder
            .HasDiscriminator(x => x.InfoType)
            .HasValue<InfoText>(EntryInfoTypeEnum.Text)
            .HasValue<InfoDate>(EntryInfoTypeEnum.Date)
            ;
        
        builder
            .Property(x => x.InfoType)
            .HasConversion(new EnumToStringConverter<EntryInfoTypeEnum>());
    }
}
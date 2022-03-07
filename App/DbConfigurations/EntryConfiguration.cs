using App.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace App.DbConfigurations;

public class EntryConfiguration : IEntityTypeConfiguration<Entry>
{
    public void Configure(EntityTypeBuilder<Entry> builder)
    {
        // builder
        //     .HasMany(x => x.Tags)
        //     .WithMany(x => x.Entries)
        //     .UsingEntity(j => j.ToTable("EntryEntryTag"))
        //     ;
        
        builder
            .HasDiscriminator(x => x.EntryType)
            .HasValue<Person>(EntryTypeEnum.Person)
            .HasValue<Meet>(EntryTypeEnum.Meet)
            .HasValue<Company>(EntryTypeEnum.Company)
            ;

        builder
            .Property(x => x.EntryType)
            .HasConversion(new EnumToStringConverter<EntryTypeEnum>());
    }
}
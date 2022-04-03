using App.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace App.DbConfigurations;

public class EntryInfoConfiguration : IEntityTypeConfiguration<EntryInfo>
{
    public void Configure(EntityTypeBuilder<EntryInfo> builder)
    {
        builder.ToTable("EntryInfos");

        builder.HasDiscriminator(x => x.Discriminator)
            .HasValue<EntryInfoEmail>(EntryInfo.EmailType)
            .HasValue<EntryInfoPhone>(EntryInfo.PhoneType)
            .HasValue<EntryInfoUrl>(EntryInfo.UrlType)
            .HasValue<EntryInfoNote>(EntryInfo.NoteType)
            .HasValue<EntryInfoDate>(EntryInfo.DateType)
            ;
    }
}
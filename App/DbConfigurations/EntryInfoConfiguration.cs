﻿using App.Models;
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
            .HasValue<EntryEmail>(EntryInfo.EmailType)
            .HasValue<EntryPhone>(EntryInfo.PhoneType)
            .HasValue<EntryUrl>(EntryInfo.UrlType)
            .HasValue<EntryNote>(EntryInfo.NoteType)
            .HasValue<EntryDate>(EntryInfo.DateType)
            ;
    }
}
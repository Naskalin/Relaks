﻿using App.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace App.DbConfigurations;

public class AppDbContext : DbContext
{
    public DbSet<Entry> Entries { get; set; } = null!;

    public DbSet<EntryInfoDate> EntryInfoDates { get; set; } = null!;
    public DbSet<EntryInfoNote> EntryInfoNotes { get; set; } = null!;
    public DbSet<EntryInfoPhone> EntryInfoPhones { get; set; } = null!;
    public DbSet<EntryInfoEmail> EntryInfoEmails { get; set; } = null!;
    public DbSet<EntryInfoUrl> EntryInfoUrls { get; set; } = null!;
    
    // public DbSet<EntryDate> EntryDates { get; set; } = null!;
    // public DbSet<EntryText> EntryTexts { get; set; } = null!;
    // public DbSet<EntryTag> EntryTags { get; set; } = null!;

    public DbSet<EntryFile> EntryFiles { get; set; } = null!;
    public DbSet<EntryInfoFile> EntryInfoFiles { get; set; } = null!;

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new EntryConfiguration());
        // modelBuilder.ApplyConfiguration(new EntryDateConfiguration());
        // modelBuilder.ApplyConfiguration(new EntryTextConfiguration());
        modelBuilder.ApplyConfiguration(new EntryInfoConfiguration());
        modelBuilder.ApplyConfiguration(new FileConfiguration());
    }
}
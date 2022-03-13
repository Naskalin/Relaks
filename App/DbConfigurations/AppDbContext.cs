using App.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace App.DbConfigurations;

public class AppDbContext : DbContext
{
    public DbSet<Entry> Entries { get; set; } = null!;
    
    public DbSet<EntryInfo> EntryInfos { get; set; } = null!;
    public DbSet<InfoDate> InfoDates { get; set; } = null!;
    public DbSet<InfoText> InfoTexts { get; set; } = null!;
    
    public DbSet<EntryTag> EntryTags { get; set; } = null!;

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<InfoDate>()
            .Property(x => x.DateType)
            .HasConversion(new EnumToStringConverter<InfoDateTypeEnum>());
        
        modelBuilder
            .Entity<InfoText>()
            .Property(x => x.TextType)
            .HasConversion(new EnumToStringConverter<InfoTextTypeEnum>());
        
        modelBuilder.ApplyConfiguration(new EntryConfiguration());
        modelBuilder.ApplyConfiguration(new EntryInfoConfiguration());
    }
}
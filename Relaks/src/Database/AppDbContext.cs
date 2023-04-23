using Microsoft.EntityFrameworkCore;
using Relaks.Database.Configurations;
using Relaks.Database.Events;
using Relaks.Models;

namespace Relaks.Database;

public sealed class AppDbContext : DbContext
{
    // public DbSet<Test> Tests { get; set; } = null!;
    public DbSet<Entry> Entries { get; set; } = null!;
    
    public DbSet<EntryInfo> EntryInfos { get; set; } = null!;
    public DbSet<EiPhone> EiPhones { get; set; } = null!;
    public DbSet<EiEmail> EiEmails { get; set; } = null!;
    public DbSet<EiDate> EiDates { get; set; } = null!;
    public DbSet<EiUrl> EiUrls { get; set; } = null!;
    public DbSet<EiCustom> EiCustoms { get; set; } = null!;

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        SavingChanges += AppEventSpreader.OnSavingChanges;
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new EntryConfiguration());
        modelBuilder.ApplyConfiguration(new EntryInfoConfiguration());

        // DateTime Always UTC
        // https://stackoverflow.com/a/61243301/5638975
        // https://github.com/dotnet/efcore/issues/4711#issuecomment-589842988
        // var dateTimeConverter = new ValueConverter<DateTime, DateTime>(
        //     v => v.ToUniversalTime(),
        //     v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
        //
        // var nullableDateTimeConverter = new ValueConverter<DateTime?, DateTime?>(
        //     v => v.HasValue ? v.Value.ToUniversalTime() : v,
        //     v => v.HasValue ? DateTime.SpecifyKind(v.Value, DateTimeKind.Utc) : v);
        //
        // foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        // {
        //     if (entityType.IsKeyless)
        //     {
        //         continue;
        //     }
        //
        //     foreach (var property in entityType.GetProperties())
        //     {
        //         if (property.ClrType == typeof(DateTime))
        //         {
        //             property.SetValueConverter(dateTimeConverter);
        //         }
        //         else if (property.ClrType == typeof(DateTime?))
        //         {
        //             property.SetValueConverter(nullableDateTimeConverter);
        //         }
        //     }
        // }
        // -- DateTime Always UTC
    }
}
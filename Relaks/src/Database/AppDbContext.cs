using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Relaks.Database.Configurations;
using Relaks.Database.Events;
using Relaks.Models;

namespace Relaks.Database;

public sealed class AppDbContext : DbContext
{
    public DbSet<BaseEntry> BaseEntries { get; set; } = null!;
    public DbSet<ECompany> ECompanies { get; set; } = null!;
    public DbSet<EPerson> EPersons { get; set; } = null!;
    public DbSet<EMeet> EMeets { get; set; } = null!;
    public DbSet<Country> Countries { get; set; } = null!;

    public DbSet<BaseEntryInfo> BaseEntryInfos { get; set; } = null!;
    public DbSet<EiPhone> EiPhones { get; set; } = null!;
    public DbSet<EiEmail> EiEmails { get; set; } = null!;
    public DbSet<EiDate> EiDates { get; set; } = null!;
    public DbSet<EiUrl> EiUrls { get; set; } = null!;
    public DbSet<EiDataset> EiDatasets { get; set; } = null!;

    public DbSet<BaseFile> BaseFiles { get; set; } = null!;
    public DbSet<EntryFile> EntryFiles { get; set; } = null!;
    
    public DbSet<BaseFileCategory> BaseFileCategories { get; set; } = null!;
    public DbSet<EntryFileCategory> EntryFileCategories { get; set; } = null!;
    
    public DbSet<BaseFileTag> BaseFileTags { get; set; } = null!;
    public DbSet<EntryFileTag> EntryFileTags { get; set; } = null!;

    [DbFunction]
    public string Snippet(string match, string column, string open, string close, string ellips, int count)
        => throw new NotImplementedException();

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        SavingChanges += AppEventSpreader.OnSavingChanges;
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("NOCASE");
        modelBuilder.ApplyConfiguration(new FtsEntryConfiguration());
        modelBuilder.ApplyConfiguration(new FtsEntryInfoConfiguration());
        modelBuilder.ApplyConfiguration(new FtsFileConfiguration());
        modelBuilder.ApplyConfiguration(new FileCategoryConfiguration());

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
        
    }
}
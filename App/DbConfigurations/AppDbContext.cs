using App.DbEvents;
using App.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace App.DbConfigurations;

public sealed class AppDbContext : DbContext
{
    public DbSet<Entry> Entries { get; set; } = null!;
    public DbSet<EntryInfo> EntryInfos { get; set; } = null!;
    public DbSet<FileModel> FileModels { get; set; } = null!;
    public DbSet<EntryFile> EntryFiles { get; set; } = null!;
    public DbSet<EntryInfoFile> EntryInfoFiles { get; set; } = null!;
    public DbSet<InfoTemplate> InfoTemplates { get; set; } = null!;

    public DbSet<Structure> Structures { get; set; } = null!;
    public DbSet<StructureItem> StructureItems { get; set; } = null!;
    public DbSet<StructureConnection> StructureConnections { get; set; } = null!;

    [DbFunction]
    public string Snippet(string match, string column, string open, string close, string ellips, int count)
        => throw new NotImplementedException();

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        SavingChanges += EventSpreader.OnSavingChanges;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new EntryConfiguration());
        modelBuilder.ApplyConfiguration(new FtsEntryConfiguration());
        
        modelBuilder.ApplyConfiguration(new EntryInfoConfiguration());
        modelBuilder.ApplyConfiguration(new FtsEntryInfoConfiguration());
        
        modelBuilder.ApplyConfiguration(new FileConfiguration());
        modelBuilder.ApplyConfiguration(new StructureConfiguration());
        modelBuilder.ApplyConfiguration(new StructureConnectionConfiguration());
        
        // DateTime Always UTC
        // https://stackoverflow.com/a/61243301/5638975
        // https://github.com/dotnet/efcore/issues/4711#issuecomment-589842988
        var dateTimeConverter = new ValueConverter<DateTime, DateTime>(
            v => v.ToUniversalTime(),
            v => DateTime.SpecifyKind(v, DateTimeKind.Utc));

        var nullableDateTimeConverter = new ValueConverter<DateTime?, DateTime?>(
            v => v.HasValue ? v.Value.ToUniversalTime() : v,
            v => v.HasValue ? DateTime.SpecifyKind(v.Value, DateTimeKind.Utc) : v);

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (entityType.IsKeyless)
            {
                continue;
            }

            foreach (var property in entityType.GetProperties())
            {
                if (property.ClrType == typeof(DateTime))
                {
                    property.SetValueConverter(dateTimeConverter);
                }
                else if (property.ClrType == typeof(DateTime?))
                {
                    property.SetValueConverter(nullableDateTimeConverter);
                }
            }
        }
        // -- DateTime Always UTC
    }
}
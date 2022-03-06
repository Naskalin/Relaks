using App.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace App.Data;

public class AppDbContext : DbContext
{
    public virtual DbSet<BaseEntry> Entries { get; set; } = null!;
    public DbSet<Person> Persons { get; set; } = null!;
    public DbSet<Meet> Meets { get; set; } = null!;
    public DbSet<Company> Companies { get; set; } = null!;
    public DbSet<Note> Notes { get; set; } = null!;

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        // Database.EnsureDeleted();
        // Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // modelBuilder.Entity<BaseEntry>()
        //     .Property(x => x.Name)
        //     .UseCollation("NOCASE")
        //     ;
        
        modelBuilder.Entity<BaseEntry>()
            .HasDiscriminator(x => x.EntryType)
            .HasValue<Person>(EntryTypeEnum.Person)
            .HasValue<Meet>(EntryTypeEnum.Meet)
            .HasValue<Company>(EntryTypeEnum.Company)
            ;

        modelBuilder.Entity<BaseEntry>()
            .Property(x => x.EntryType)
            .HasConversion(new EnumToStringConverter<EntryTypeEnum>());
    }
}
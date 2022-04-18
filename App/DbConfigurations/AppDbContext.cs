using App.Models;
using Microsoft.EntityFrameworkCore;

namespace App.DbConfigurations;

public class AppDbContext : DbContext
{
    public DbSet<Entry> Entries { get; set; } = null!;

    public DbSet<EntryInfo> EntryInfos { get; set; } = null!;
    public DbSet<EntryDate> EntryDates { get; set; } = null!;
    public DbSet<EntryNote> EntryNotes { get; set; } = null!;
    public DbSet<EntryPhone> EntryPhones { get; set; } = null!;
    public DbSet<EntryEmail> EntryEmails { get; set; } = null!;
    public DbSet<EntryUrl> EntryUrls { get; set; } = null!;

    public DbSet<EntryFile> EntryFiles { get; set; } = null!;
    public DbSet<EntryInfoFile> EntryInfoFiles { get; set; } = null!;

    // public DbSet<Post> Posts { get; set; } = null!;
    
    // [DbFunction]
    // public string Highlight(string match, string column, string open, string close)
    //     => throw new NotImplementedException();

    [DbFunction]
    public string Snippet(string match, string column, string open, string close, string ellips, int count)
        => throw new NotImplementedException();

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new EntryConfiguration());
        modelBuilder.ApplyConfiguration(new EntryFtsConfiguration());
        
        modelBuilder.ApplyConfiguration(new EntryInfoConfiguration());
        modelBuilder.ApplyConfiguration(new EntryInfoFtsConfiguration());
        
        modelBuilder.ApplyConfiguration(new FileConfiguration()); 

        // FTS
        // modelBuilder.Entity<PostFts>(builder =>
        // {
        //     builder.HasKey(fts => fts.RowId);
        //
        //     builder
        //         .Property(fts => fts.Match)
        //         .HasColumnName(nameof(PostFts));
        // });
    }
}
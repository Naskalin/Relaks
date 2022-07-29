using App.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.DbConfigurations;

public class FileCategoryConfiguration : IEntityTypeConfiguration<FileCategory>
{
    public void Configure(EntityTypeBuilder<FileCategory> builder)
    {
        builder.ToTable("FileCategories");
        
        builder
            .HasMany(x => x.Children)
            .WithOne(x => x.Parent)
            .HasForeignKey(x => x.ParentId)
            .OnDelete(DeleteBehavior.Cascade)
            ;
    }
}
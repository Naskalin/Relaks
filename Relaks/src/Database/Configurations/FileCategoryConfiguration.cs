using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Relaks.Models;

namespace Relaks.Database.Configurations;

public class FileCategoryConfiguration : IEntityTypeConfiguration<BaseFileCategory>
{
    public void Configure(EntityTypeBuilder<BaseFileCategory> builder)
    {
        builder
            .HasMany(x => x.Children)
            .WithOne(x => x.Parent)
            .HasForeignKey(x => x.ParentId)
            .OnDelete(DeleteBehavior.Cascade)
            ;
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Relaks.Models;

namespace Relaks.Database.Configurations;

public class EntryTagCategoryConfiguration : IEntityTypeConfiguration<EntryTagCategory>
{
    public void Configure(EntityTypeBuilder<EntryTagCategory> builder)
    {
        builder
            .HasMany(x => x.Children)
            .WithOne(x => x.Parent)
            .HasForeignKey(x => x.ParentId)
            .OnDelete(DeleteBehavior.Cascade)
            ;
    }
}
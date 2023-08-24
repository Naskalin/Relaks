using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Relaks.Models.StructureModels;

namespace Relaks.Database.Configurations;

public class StructureGroupConfiguration : IEntityTypeConfiguration<StructureGroup>
{
    public void Configure(EntityTypeBuilder<StructureGroup> builder)
    {
        builder
            .HasMany(x => x.Children)
            .WithOne(x => x.Parent)
            .HasForeignKey(x => x.ParentId)
            .OnDelete(DeleteBehavior.Cascade)
            ;
    }
}
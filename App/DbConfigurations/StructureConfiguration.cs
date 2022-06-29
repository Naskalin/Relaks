using App.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace App.DbConfigurations;

public class StructureConfiguration : IEntityTypeConfiguration<Structure>
{
    public void Configure(EntityTypeBuilder<Structure> builder)
    {
        builder
            .HasMany(x => x.Children)
            .WithOne(x => x.Parent)
            .HasForeignKey(x => x.ParentId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

public class StructureConnectionConfiguration : IEntityTypeConfiguration<StructureConnection>
{
    public void Configure(EntityTypeBuilder<StructureConnection> builder)
    {
        builder
            .Property(x => x.Direction)
            .HasConversion(new EnumToStringConverter<StructureConnection.DirectionEnum>());
    }
}
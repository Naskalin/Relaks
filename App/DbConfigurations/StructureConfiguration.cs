using App.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace App.DbConfigurations;

public class StructureConnectionConfiguration : IEntityTypeConfiguration<StructureConnection>
{
    public void Configure(EntityTypeBuilder<StructureConnection> builder)
    {
        builder
            .Property(x => x.Direction)
            .HasConversion(new EnumToStringConverter<StructureConnection.DirectionEnum>());
    }
}
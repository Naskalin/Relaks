using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Relaks.Models;

namespace Relaks.Database.Configurations;

public class EntryRelationConfiguration : IEntityTypeConfiguration<EntryRelation>
{
    public void Configure(EntityTypeBuilder<EntryRelation> builder)
    {
        builder.HasIndex(x => new {x.FirstId, x.SecondId}).IsUnique();
    }
}
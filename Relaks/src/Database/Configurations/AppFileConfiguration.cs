using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Relaks.Models;

namespace Relaks.Database.Configurations;

public class EntryFileConfiguration : IEntityTypeConfiguration<EntryFile>
{
    public void Configure(EntityTypeBuilder<EntryFile> builder)
    {
        builder.HasOne(x => x.Entry).WithMany(x => x.EntryFiles);
    }
}

public class BaseFileConfiguration : IEntityTypeConfiguration<BaseFile>
{
    public void Configure(EntityTypeBuilder<BaseFile> builder)
    {
        builder.HasMany(x => x.BaseEntryRelations).WithMany(x => x.BaseFileRelations);
    }
}

using Fearlessforever.Databases.Shared.Models.Testing;
using Fearlessforever.Shared.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fearlessforever.Databases.Contexts.Configurations.InMemory;

public class InMemoryKeyIntItemConfig : IEntityTypeConfiguration<InMemoryKeyIntItem>
{
  public void Configure(EntityTypeBuilder<InMemoryKeyIntItem> builder)
  {
    builder.HasQueryFilter(x => x.IsDeleted == false || x.IsDeleted == null);
    builder.HasIndex(x => x.IsDeleted);
  }
}

public class InMemoryKeyGuidItemConfig : IEntityTypeConfiguration<InMemoryKeyGuidItem>
{
  public void Configure(EntityTypeBuilder<InMemoryKeyGuidItem> builder)
  {
    builder.HasQueryFilter(x => x.IsDeleted == false || x.IsDeleted == null);
    builder.HasIndex(x => x.IsDeleted);
  }
}

public class InMemoryKeyByteItemConfig : IEntityTypeConfiguration<InMemoryKeyByteItem>
{
  public void Configure(EntityTypeBuilder<InMemoryKeyByteItem> builder)
  {
    builder.Property(x => x.Id).ValueGeneratedOnAdd().HasDefaultValue( MySecurityHelper.GenerateUUID().ToByteArray() );
    builder.HasQueryFilter(x => x.IsDeleted == false || x.IsDeleted == null);
    builder.HasIndex(x => x.IsDeleted);
  }
}
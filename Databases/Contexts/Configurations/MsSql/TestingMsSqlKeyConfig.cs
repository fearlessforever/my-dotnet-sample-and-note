using Fearlessforever.Databases.Shared.Models.Testing;
using Fearlessforever.Databases.Utils;
using Fearlessforever.Shared.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fearlessforever.Databases.Contexts.Configurations.MsSql;

public class PostgreKeyByteItemConfig : IEntityTypeConfiguration<MsSqlKeyByteItem>
{
  public void Configure(EntityTypeBuilder<MsSqlKeyByteItem> builder)
  {
    builder.ToTable("MsSqlKeyByteItems");
    builder.HasKey(x => x.Id);
    builder.Property(x => x.Id).ValueGeneratedOnAdd().HasValueGenerator<GeneratorByteIdValueGenerator>();
    
    builder.Property(x => x.DateCreated).HasDefaultValueSql(MyConstants.CurrentTimeStampMsSql);
    builder.HasQueryFilter(x => x.IsDeleted == false || x.IsDeleted == null);
    builder.HasIndex(x => x.IsDeleted);
  }
}

public class MsSqlKeyGuidItemConfig : IEntityTypeConfiguration<MsSqlKeyGuidItem>
{
  public void Configure(EntityTypeBuilder<MsSqlKeyGuidItem> builder)
  {
    builder.ToTable("MsSqlKeyGuidItems");
    builder.HasKey(x => x.Id);
    builder.Property(x => x.DateCreated).HasDefaultValueSql(MyConstants.CurrentTimeStampMsSql);
    builder.HasQueryFilter(x => x.IsDeleted == false || x.IsDeleted == null);
    builder.HasIndex(x => x.IsDeleted);
  }
}

// public class ViewPostgreKeyItemConfig : IEntityTypeConfiguration<ViewPostgreKeyItem>
// {
//   public void Configure(EntityTypeBuilder<ViewPostgreKeyItem> builder)
//   {
//     builder.ToView("ViewKeyItems");
//     builder.HasNoKey();
//   }
// }


using Fearlessforever.Databases.Shared.Models.Testing;
using Fearlessforever.Databases.Utils;
using Fearlessforever.Shared.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fearlessforever.Databases.Contexts.Configurations.PostgreSql;

public class PostgreKeyByteItemConfig : IEntityTypeConfiguration<PostgreKeyByteItem>
{
  public void Configure(EntityTypeBuilder<PostgreKeyByteItem> builder)
  {
    builder.ToTable("PostgreKeyByteItems");
    builder.HasKey(x => x.Id);
    builder.Property(x => x.Id).ValueGeneratedOnAdd().HasValueGenerator<GeneratorByteIdValueGenerator>();

    // using this one give warning for postgre-sql
    // builder.Property(x => x.Id).ValueGeneratedOnAdd().HasDefaultValue(MySecurityHelper.GenerateUUID().ToByteArray());
    
    builder.Property(x => x.DateCreated).HasDefaultValueSql(MyConstants.CurrentTimeStampPostgreSql);
    builder.HasQueryFilter(x => x.IsDeleted == false || x.IsDeleted == null);
    builder.HasIndex(x => x.IsDeleted);
  }
}

public class PostgreKeyGuidItemConfig : IEntityTypeConfiguration<PostgreKeyGuidItem>
{
  public void Configure(EntityTypeBuilder<PostgreKeyGuidItem> builder)
  {
    builder.ToTable("PostgreKeyGuidItems");
    builder.HasKey(x => x.Id);
    builder.Property(x => x.DateCreated).HasDefaultValueSql(MyConstants.CurrentTimeStampPostgreSql);
    builder.HasQueryFilter(x => x.IsDeleted == false || x.IsDeleted == null);
    builder.HasIndex(x => x.IsDeleted);
  }
}

public class ViewPostgreKeyItemConfig : IEntityTypeConfiguration<ViewPostgreKeyItem>
{
  public void Configure(EntityTypeBuilder<ViewPostgreKeyItem> builder)
  {
    builder.ToView("ViewKeyItems");
    builder.HasNoKey();
    // builder.Property(x => x.CreatedBy).HasColumnName("CREATED_BY");
    // builder.Property(x => x.Id).HasConversion<BytesToBase64StringConverter>();
  }
}
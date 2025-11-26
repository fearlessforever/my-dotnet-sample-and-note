using Fearlessforever.Databases.Shared.Models.Testing;
using Fearlessforever.Shared.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fearlessforever.Databases.Contexts.Configurations.Sqlite;

public class SqliteKeyByteItemConfig : IEntityTypeConfiguration<SqliteKeyByteItem>
{
  public void Configure(EntityTypeBuilder<SqliteKeyByteItem> builder)
  {
    builder.ToTable("SqliteKeyByteItems");
    builder.HasKey(x => x.Id);
    builder.Property(x => x.DateCreated).HasDefaultValueSql(MyConstants.CurrentTimeStampSqlite);
    builder.HasQueryFilter(x => x.IsDeleted == false || x.IsDeleted == null);
    builder.HasIndex(x => x.IsDeleted);
  }
}

public class SqliteKeyStringItemConfig : IEntityTypeConfiguration<SqliteKeyStringItem>
{
  public void Configure(EntityTypeBuilder<SqliteKeyStringItem> builder)
  {
    builder.ToTable("SqliteKeyStringItems");
    builder.HasKey(x => x.Id);
    builder.Property(x => x.DateCreated).HasDefaultValueSql(MyConstants.CurrentTimeStampSqlite);
    builder.HasQueryFilter(x => x.IsDeleted == false || x.IsDeleted == null);
    builder.HasIndex(x => x.IsDeleted);
  }
}

public class SqliteKeyGuidItemConfig : IEntityTypeConfiguration<SqliteKeyGuidItem>
{
  public void Configure(EntityTypeBuilder<SqliteKeyGuidItem> builder)
  {
    builder.ToTable("SqliteKeyGuidItems");
    builder.HasKey(x => x.Id);
    builder.Property(x => x.DateCreated).HasDefaultValueSql(MyConstants.CurrentTimeStampSqlite);
    builder.HasQueryFilter(x => x.IsDeleted == false || x.IsDeleted == null);
    builder.HasIndex(x => x.IsDeleted);
  }
}

public class ViewSqliteKeyItemConfig : IEntityTypeConfiguration<ViewSqliteKeyItem>
{
  public void Configure(EntityTypeBuilder<ViewSqliteKeyItem> builder)
  {
    builder.ToView("ViewSqliteKeyItem");
    builder.HasNoKey();
    // builder.Property(x => x.CreatedBy).HasColumnName("CREATED_NY");
    // builder.Property(x => x.Id).HasConversion<BytesToBase64StringConverter>();
  }
}

// public class BytesToBase64StringConverter : Microsoft.EntityFrameworkCore.Storage.ValueConversion.ValueConverter<byte[], string>
// {
//     public BytesToBase64StringConverter()
//         : base(
//             v => Convert.ToBase64String(v), // Convert byte[] to string for the database
//             v => Convert.FromBase64String(v)  // Convert string back to byte[] for the entity
//         )
//     { }
// }
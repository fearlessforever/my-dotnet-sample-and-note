
using Fearlessforever.Databases.Shared.Models;
using Fearlessforever.Shared.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fearlessforever.Databases.Contexts.Configurations.Sqlite;
public class AuthorConfig : IEntityTypeConfiguration<Author>
{
  public void Configure(EntityTypeBuilder<Author> builder)
  {
    builder.ToTable("Authors");
    builder.HasKey(x => x.Id);
    builder.Property(x => x.DateCreated).HasDefaultValueSql(MyConstants.CurrentTimeStampSqlite);
    builder.HasQueryFilter(x => x.IsDeleted == false || x.IsDeleted == null);
    builder.HasIndex(x => x.IsDeleted);

    builder.HasMany(x => x.Blogs).WithOne(x => x.Author);
  }
}
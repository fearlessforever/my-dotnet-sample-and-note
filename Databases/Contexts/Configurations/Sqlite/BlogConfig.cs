
using Fearlessforever.Databases.Shared.Models;
using Fearlessforever.Shared.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fearlessforever.Databases.Contexts.Configurations.Sqlite;
public class BlogConfig : IEntityTypeConfiguration<Blog>
{
  public void Configure(EntityTypeBuilder<Blog> builder)
  {
    builder.ToTable("Blogs");
    builder.HasKey(x => x.Id);
    builder.Property(x => x.DateCreated).HasDefaultValueSql(MyConstants.CurrentTimeStampSqlite);
    builder.HasQueryFilter(x => x.IsDeleted == false || x.IsDeleted == null);
    builder.HasIndex(x => x.IsDeleted);

    builder.HasOne(x => x.Author).WithMany(x => x.Blogs).HasForeignKey(x => x.AuthorId);
  }
}
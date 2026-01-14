

using Fearlessforever.Databases.Shared.Models;
using Fearlessforever.Shared.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fearlessforever.Databases.Contexts.Configurations.MsSql;
public class BookCategoryConfig : IEntityTypeConfiguration<BookCategory>
{
  public void Configure(EntityTypeBuilder<BookCategory> builder)
  {
    builder.ToTable("BookCategories", schema: "CityLibrary");
    builder.HasKey(x => x.Id);
    builder.Property(x => x.DateCreated).HasDefaultValueSql(MyConstants.CurrentTimeStampMsSql);
    builder.HasQueryFilter(x => x.IsDeleted == false || x.IsDeleted == null);
    builder.HasIndex(x => x.IsDeleted);

    builder.HasOne(x => x.Book).WithMany(x => x.BookCategories).HasForeignKey(x => x.BookId);
  }
}
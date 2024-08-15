using Fearlessforever.Databases.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace Fearlessforever.Databases.Contexts;

public sealed class AppInMemoryContext(DbContextOptions<AppInMemoryContext> options) : DbContext(options)
{
  public DbSet<Book> Books { get; set; }
  public DbSet<BookCategory> BookCategories { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);
    modelBuilder.ApplyConfigurationsForEntitiesInContext();
  }
}
using Fearlessforever.Databases.Shared.Models;
using Fearlessforever.Databases.Shared.Models.Testing;
using Microsoft.EntityFrameworkCore;

namespace Fearlessforever.Databases.Contexts;

public sealed class AppSqliteContext(DbContextOptions<AppSqliteContext> options) : DbContext(options)
{
  public DbSet<Blog> Blogs { get; set; }
  public DbSet<Author> Authors { get; set; }
  public DbSet<SqliteKeyByteItem> SqliteKeyByteItems { get; set; }
  public DbSet<SqliteKeyStringItem> SqliteKeyStringItems { get; set; }
  public DbSet<SqliteKeyGuidItem> SqliteKeyGuidItems { get; set; }
  public DbSet<ViewSqliteKeyItem> ViewSqliteKeyItems { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);
    modelBuilder.ApplyConfigurationsForEntitiesInContext();
  }
}
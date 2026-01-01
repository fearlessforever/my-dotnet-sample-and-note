using Fearlessforever.Databases.Shared.Models.Testing;
using Microsoft.EntityFrameworkCore;

namespace Fearlessforever.Databases.Contexts;

public sealed class AppInMemoryContext(DbContextOptions<AppInMemoryContext> options) : DbContext(options)
{
  public DbSet<InMemoryKeyIntItem> InMemoryKeyIntItems { get; set; }
  public DbSet<InMemoryKeyByteItem> InMemoryKeyByteItems { get; set; }
  public DbSet<InMemoryKeyGuidItem> InMemoryKeyGuidItems { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);
    modelBuilder.ApplyConfigurationsForEntitiesInContext();
  }
}
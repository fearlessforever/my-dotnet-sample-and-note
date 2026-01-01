using Fearlessforever.Databases.Shared.Models;
using Fearlessforever.Databases.Shared.Models.Testing;
using Microsoft.EntityFrameworkCore;

namespace Fearlessforever.Databases.Contexts;

public sealed class AppPostgreSqlContext(DbContextOptions<AppPostgreSqlContext> options) : DbContext(options)
{
  public DbSet<Project> Projects { get; set; }
  public DbSet<Ticket> Tickets { get; set; }
  public DbSet<PostgreKeyByteItem> PostgreKeyByteItems { get; set; }
  public DbSet<PostgreKeyGuidItem> PostgreKeyGuidItems { get; set; }
  public DbSet<ViewPostgreKeyItem> ViewPostgreKeyItems { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);
    modelBuilder.ApplyConfigurationsForEntitiesInContext();
  }
}
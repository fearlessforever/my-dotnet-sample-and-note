using Fearlessforever.Databases.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace Fearlessforever.Databases.Contexts;

public sealed class AppPostgreSqlContext(DbContextOptions<AppPostgreSqlContext> options) : DbContext(options)
{
  public DbSet<Project> Projects { get; set; }
  public DbSet<Ticket> Tickets { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);
    modelBuilder.ApplyConfigurationsForEntitiesInContext();
  }
}
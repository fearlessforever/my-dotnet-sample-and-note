
namespace Fearlessforever.Shared.Configs;

public class FeaturesConfig
{
  public bool ApplyDatabasesMigrationsAndSeederWhenAppStart { get; set; } = false;
  public bool UseRedisCache { get; set; } = false;
  public bool UseDatabaseSqlite { get; set; } = false;
  public bool UseDatabaseInMemory { get; set; } = false;
  public bool UseDatabaseMsSql { get; set; } = false;
  public bool UseDatabasePostgreSql { get; set; } = false;
  public bool UseQueueHangFire { get; set; } = false;
  public bool UseSignalR { get; set; } = false;
}
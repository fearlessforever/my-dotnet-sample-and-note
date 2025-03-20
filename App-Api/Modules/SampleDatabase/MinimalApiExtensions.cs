
using static Fearlessforever.Api.Modules.SampleDatabase.Sqlite.MinimalApiExtensions;
using static Fearlessforever.Api.Modules.SampleDatabase.InMemory.MinimalApiExtensions;
using static Fearlessforever.Api.Modules.SampleDatabase.PostgreSql.MinimalApiExtensions;
using static Fearlessforever.Api.Modules.SampleDatabase.MsSql.MinimalApiExtensions;

namespace Fearlessforever.Api.Modules.SampleDatabase;

public static partial class MinimalApiExtensions
{
  public static IEndpointConventionBuilder MapMiniApiSampleDatabase(this IEndpointRouteBuilder endpoints)
  {
    var sampleDatabases = endpoints.MapGroup("SampleDatabase");

    MapMiniApiSampleDatabaseSqlite(sampleDatabases);
    MapMiniApiSampleDatabaseInMemory(sampleDatabases);
    MapMiniApiSampleDatabasePostgreSql(sampleDatabases);
    MapMiniApiSampleDatabaseMsSql(sampleDatabases);

    return sampleDatabases;
  }
}


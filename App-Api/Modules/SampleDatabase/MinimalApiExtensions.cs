
using static Fearlessforever.Api.Modules.SampleDatabase.Sqlite.MinimalApiExtensions;

namespace Fearlessforever.Api.Modules.SampleDatabase;

public static partial class MinimalApiExtensions
{
  public static IEndpointConventionBuilder MapMiniApiSampleDatabase(this IEndpointRouteBuilder endpoints)
  {
    var sampleDatabases = endpoints.MapGroup("SampleDatabase");

    MapMiniApiSampleDatabaseSqlite(sampleDatabases);

    return sampleDatabases;
  }
}


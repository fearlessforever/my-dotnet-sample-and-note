using Mapster;
using System.Reflection;
using Fearlessforever.Databases.Contexts;
using Fearlessforever.Databases.Services;
using Fearlessforever.Shared.Configs;
using Fearlessforever.Shared.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MapsterMapper;
using Fearlessforever.Databases.Services.Testing;
using Fearlessforever.Databases.Utils;
using System.Diagnostics;

namespace Fearlessforever.Databases;

public static partial class ApplicationServiceExtensions
{
  private static FeaturesConfig FeaturesConfig = new();
  public static void ApplyConfigurationsForEntitiesInContext(this ModelBuilder modelBuilder)
  {
    var types = modelBuilder.Model.GetEntityTypes().Select(t => t.ClrType).ToHashSet();

    modelBuilder.ApplyConfigurationsFromAssembly(
            Assembly.GetExecutingAssembly(),
            t => t.GetInterfaces()
                .Any(i => i.IsGenericType
                    && i.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)
                    && types.Contains(i.GenericTypeArguments[0]))
        );
  }

  public static void AddCoreDatabaseServices(this IServiceCollection services, IConfiguration configuration)
  {
    FeaturesConfig = configuration.GetSection("Features").Get<FeaturesConfig>() ?? new();
    string? assembly = typeof(IMyDatabases).GetTypeInfo().Assembly.GetName().Name;

    string? checkDbConnectionSqlite = configuration.GetConnectionString("Sqlite");
    if (FeaturesConfig.UseDatabaseSqlite && !string.IsNullOrEmpty(checkDbConnectionSqlite))
    {
      DebugHelper.Log("Sqlite is loaded", "Database Configuration");
      services.AddDbContextPool<AppSqliteContext>((provider, options) =>
      {
        options.UseSqlite(checkDbConnectionSqlite, t => t.MigrationsAssembly(assembly));
        options.AddInterceptors(provider.GetRequiredService<MySaveChangesInterceptorSingleton>());

        // options.ConfigureWarnings( warnings =>{
        //   warnings.Log( Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId. );
        // });
      });
    }
    else
    {
      FeaturesConfig.UseDatabaseSqlite = false;
    }

    string? checkDbConnectionMsSql = configuration.GetConnectionString("MsSql");
    if (FeaturesConfig.UseDatabaseMsSql && !string.IsNullOrEmpty(checkDbConnectionMsSql))
    {
      DebugHelper.Log("Ms-SQL is loaded", "Database Configuration");
      services.AddDbContext<AppMsSqlContext>((provider, options) =>
      {
        options.UseSqlServer(checkDbConnectionMsSql, t => t.MigrationsAssembly(assembly))
               .AddInterceptors(provider.GetRequiredService<MySaveChangesInterceptor>());
      });
    }
    else
    {
      FeaturesConfig.UseDatabaseMsSql = false;
    }

    string? checkDbConnectionPostgreSql = configuration.GetConnectionString("PostgreSql");
    if (FeaturesConfig.UseDatabasePostgreSql && !string.IsNullOrEmpty(checkDbConnectionPostgreSql))
    {
      DebugHelper.Log("Postgre-SQL is loaded", "Database Configuration");
      services.AddDbContext<AppPostgreSqlContext>((provider, options) =>
      {
        options.UseNpgsql(checkDbConnectionPostgreSql, t => t.MigrationsAssembly(assembly))
               .AddInterceptors(provider.GetRequiredService<MySaveChangesInterceptor>());
      });
    }
    else
    {
      FeaturesConfig.UseDatabasePostgreSql = false;
    }

    if (FeaturesConfig.UseDatabaseInMemory)
    {
      DebugHelper.Log("InMemory is loaded", "Database Configuration");
      services.AddDbContextPool<AppInMemoryContext>((provider, options) =>
      {
        options.UseInMemoryDatabase("DbNameInMemory").AddInterceptors(provider.GetRequiredService<MySaveChangesInterceptorSingleton>());
      });
    }
    else
    {
      FeaturesConfig.UseDatabaseInMemory = false;
    }
    FeatureFlagAttribute.SetFeaturesConfig(FeaturesConfig);

    RegisterAvailableServices(services);

  }

  ///
  /// Apply latest migrations & seeders to database
  ///
  public static async Task UseCoreDatabasesApplyMigrationsAndSeedersAsync(this IHost application, CancellationToken cancellationToken = default)
  {
    if (!FeaturesConfig.ApplyDatabasesMigrationsAndSeederWhenAppStart)
    {
      DebugHelper.Log("Do Not Apply Any Available Migrations And Seeders", "Database Migrations & Seeders");
      DebugHelper.Log("==================================================================================");
      return;
    }
    Stopwatch sw = Stopwatch.StartNew();
    // long mem = GC.GetAllocatedBytesForCurrentThread();
    long mem = GC.GetTotalAllocatedBytes();
    await Task.Yield();

    using var serviceProvider = application.Services.CreateScope();
    var services = serviceProvider.ServiceProvider;
    try
    {
      var loadCustomInterceptor = services.GetRequiredService<MySaveChangesInterceptor>();
      // Db Context InMemory
      if (FeaturesConfig.UseDatabaseInMemory)
      {
        DebugHelper.Log("Applying InMemory", "Database Migrations & Seeders");
        using AppInMemoryContext appInMemoryContext = services.GetRequiredService<AppInMemoryContext>();
        appInMemoryContext.Database.EnsureDeleted();
        appInMemoryContext.Database.EnsureCreated();
        appInMemoryContext.Dispose();
        DebugHelper.Log("==================================================================================");
      }

      IEnumerable<string>? checkPendingMigrations = default;
      // Db Context Sqlite
      if (FeaturesConfig.UseDatabaseSqlite)
      {
        DebugHelper.Log("Applying Sqlite", "Database Migrations & Seeders");
        using AppSqliteContext appSqliteContext = services.GetRequiredService<AppSqliteContext>();
        checkPendingMigrations = await appSqliteContext.Database.GetPendingMigrationsAsync(cancellationToken);
        if (checkPendingMigrations.Any())
        {
          await appSqliteContext.Database.MigrateAsync(cancellationToken);
          // await SeedData.Seed(context);
          // await SeedData.RegisteringModules(context);
        }
        appSqliteContext.Dispose();
        DebugHelper.Log("==================================================================================");
      }


      // Db Context PostgreSql
      if (FeaturesConfig.UseDatabasePostgreSql)
      {
        DebugHelper.Log("Applying PostgreSql", "Database Migrations & Seeders");
        using AppPostgreSqlContext appPostgreSqlContext = services.GetRequiredService<AppPostgreSqlContext>();
        checkPendingMigrations = await appPostgreSqlContext.Database.GetPendingMigrationsAsync(cancellationToken);
        if (checkPendingMigrations.Any())
        {
          await appPostgreSqlContext.Database.MigrateAsync(cancellationToken);
        }
        appPostgreSqlContext.Dispose();
        DebugHelper.Log("==================================================================================");
      }

      // Db Context Ms-Sql
      if (FeaturesConfig.UseDatabaseMsSql)
      {
        DebugHelper.Log("Applying Ms-Sql", "Database Migrations & Seeders");
        using AppMsSqlContext appMsSqlContext = services.GetRequiredService<AppMsSqlContext>();
        checkPendingMigrations = await appMsSqlContext.Database.GetPendingMigrationsAsync(cancellationToken);
        if (checkPendingMigrations.Any())
        {
          await appMsSqlContext.Database.MigrateAsync(cancellationToken);
        }
        appMsSqlContext.Dispose();
        DebugHelper.Log("==================================================================================");
      }

    }
    catch (Exception ex)
    {
      // Log any errors during seeding & migrations
      DebugHelper.Log("==================================================================================");
      DebugHelper.LogError(ex.Message, "Apply Database Migrations & Seeders");
      DebugHelper.Log("==================================================================================");
      // throw;

      ///
      /// Cancel starting up application if any error found while applying migrations & seeders
      /// 
      Environment.Exit(1);
    }

    sw.Stop();
    // mem = GC.GetAllocatedBytesForCurrentThread() - mem;
    mem = GC.GetTotalAllocatedBytes() - mem;
    DebugHelper.Log($"Running in {sw.Elapsed.TotalSeconds} secs, Used {mem} bytes , Total Current Allocated: {GC.GetTotalAllocatedBytes()} bytes", "DB Migrations Required Memory & Time");
  }

  private static void RegisterAvailableServices(IServiceCollection services)
  {
    ///
    /// Register Custom Interceptor for SoftDelete ,etc
    /// 
    services.AddSingleton<MySaveChangesInterceptorSingleton>();
    services.AddScoped<MySaveChangesInterceptor>();

    ///
    /// Mapster Profiles
    /// 
    services.AddSingleton(TypeAdapterConfig.GlobalSettings);
    services.AddScoped<IMapper, ServiceMapper>();
    var config = TypeAdapterConfig.GlobalSettings;
    Type assemblerMarker = typeof(IMyDatabases);
    config.Scan(assemblerMarker.Assembly); // Scans for all IRegister implementations

    ///
    /// Register Databases Entities Services
    ///
    
    // Sqlite
    services.AddScopedFeatureFlag<AuthorService>();
    services.AddScopedFeatureFlag<BlogService>();
    services.AddScopedFeatureFlag<SqliteKeyByteItemService>();
    services.AddScopedFeatureFlag<SqliteKeyStringItemService>();
    services.AddScopedFeatureFlag<SqliteKeyGuidItemService>();
    services.AddScopedFeatureFlag<ViewSqliteKeyService>();

    // InMemory
    services.AddScopedFeatureFlag<InMemoryKeyIntItemService>();
    services.AddScopedFeatureFlag<InMemoryKeyGuidItemService>();
    services.AddScopedFeatureFlag<InMemoryKeyByteItemService>();

    // Postgre-SQL
    services.AddScopedFeatureFlag<ProjectService>();
    services.AddScopedFeatureFlag<TicketService>();
    services.AddScopedFeatureFlag<PostgreKeyByteItemService>();
    services.AddScopedFeatureFlag<PostgreKeyGuidItemService>();
    services.AddScopedFeatureFlag<ViewPostgreKeyService>();
    
  }
  
  private static void AddScopedFeatureFlag<T>(this IServiceCollection services ) where T : class
  {
    Type myClassType = typeof(T);
    var featureFlagAttribute = (FeatureFlagAttribute?) Attribute.GetCustomAttribute(myClassType, typeof(FeatureFlagAttribute));
    if(featureFlagAttribute is not null && !featureFlagAttribute.IsFeatureEnabled() )
    {
      services.AddScoped<T>( _ => throw new InvalidOperationException($"Feature '{featureFlagAttribute.FeatureSelected}' is currently disabled."));
      return;
    }

    services.AddScoped<T>();
  }
}
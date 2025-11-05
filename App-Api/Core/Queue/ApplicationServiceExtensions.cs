
using Fearlessforever.Shared.Configs;
using Hangfire;
using Hangfire.Redis.StackExchange;

namespace Fearlessforever.Api.Core.Queue;

public static partial class ApplicationServiceExtensions
{
  private static HangfireConfigs? hangfireConfigs = null;
  public static void AddCoreQueueService(this IServiceCollection services, IConfiguration configuration)
  {
    FeaturesConfig featuresConfig = configuration.GetSection("Features").Get<FeaturesConfig>() ?? new();

    if(featuresConfig.UseQueueHangFire)
    {
      string[] allowedDBtype = ["mssql","redis"];
      hangfireConfigs = configuration.GetSection("HangfireConfigs").Get<HangfireConfigs>() ?? new();
      
      if( !allowedDBtype.Contains(hangfireConfigs.DatabaseType) )
        throw new InvalidOperationException("Hangfire Database Type in Default Configs is invalid. Try mssql or redis");
        
      string connectionStringFromEnv = Environment.GetEnvironmentVariable("HangfireConnection") ?? "";
      if( !string.IsNullOrEmpty(connectionStringFromEnv) ){
        hangfireConfigs.Connection = connectionStringFromEnv ;
      }

      services.AddHangfire((sp,config)=>{
        
        if( hangfireConfigs.DatabaseType == "mssql" )
          config.UseSqlServerStorage( hangfireConfigs.Connection );

        if( hangfireConfigs.DatabaseType == "redis" )
          config.UseRedisStorage(hangfireConfigs.Connection , new RedisStorageOptions{
            Prefix = "fearlessforever:api:{hangfire}:",
          });

          config.UseSerializerSettings(new Newtonsoft.Json.JsonSerializerSettings(){ ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore });
      });

      services.AddHangfireServer((sp,options)=>{
        options.Queues = ["default","email_queue","log_activity"];
      });
    }
    
  }

  public static void UseCoreQueueDashboard(this IApplicationBuilder app)
  {
    if(hangfireConfigs is not null){
      if( hangfireConfigs.UseHangfireDashboard )
        app.UseHangfireDashboard( pathMatch: hangfireConfigs.HangfireDashboardPath );
    }
  }
}
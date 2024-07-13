
using Fearlessforever.Shared.Configs;
using StackExchange.Redis;

namespace Fearlessforever.Api.Core.CacheProviders;

public static partial class ApplicationServiceExtensions
{
  public static void AddCoreCacheProviders(this IServiceCollection services, IConfiguration configuration)
  {
    services.AddMemoryCache();

    services.AddScoped<ICacheProvider, MemoryCacheProvider>();
    services.AddScoped<CacheManager>();

    var redisConnection = configuration.GetConnectionString("Redis");
    FeaturesConfig featuresConfig = configuration.GetSection("Features").Get<FeaturesConfig>() ?? new();
    if (featuresConfig.UseRedisCache && redisConnection != null && !string.IsNullOrEmpty(redisConnection))
    {
      // register Redis Provider
      var redisOptions = ConfigurationOptions.Parse(redisConnection);
      services.AddScoped<ICacheProvider, RedisCacheProvider>();
      services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(redisOptions));
      services.AddScoped(provider => provider.GetRequiredService<IConnectionMultiplexer>().GetDatabase());
    }
  }
}
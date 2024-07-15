
using System.Text.Json;
using StackExchange.Redis;

namespace Fearlessforever.Api.Core.CacheProviders;

public class RedisCacheProvider( IDatabase Redis , ILogger<RedisCacheProvider> logger): ICacheProvider
{
  public Task<bool> DeleteAsync(string key)
  {
    return Redis.KeyDeleteAsync(key);
  }

  public async Task<T?> GetAsync<T>(string key)
  {
    var data = await Redis.StringGetAsync(key);
    if (data.IsNullOrEmpty)
    {
      return default;
    }

    return JsonSerializer.Deserialize<T>(data!);
  }

  public async Task SaveAsync<T>(string key, T value, TimeSpan expiry)
  {
    await Redis.StringSetAsync(key, JsonSerializer.Serialize(value), expiry);
    logger.LogInformation("Data saved to Redis Storage, key: {key} expiry: {expiry}", key, expiry.TotalSeconds);
  }
}
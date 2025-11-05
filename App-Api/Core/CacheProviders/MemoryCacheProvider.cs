
using Microsoft.Extensions.Caching.Memory;

namespace Fearlessforever.Api.Core.CacheProviders;

internal sealed class MemoryCacheProvider( IMemoryCache memoryCache , ILogger<MemoryCacheProvider> logger ): ICacheProvider
{
  public Task<bool> DeleteAsync(string key)
  {
    memoryCache.Remove(key);
    return Task.FromResult(true);
  }

  public Task<T?> GetAsync<T>(string key)
  {
    memoryCache.TryGetValue(key, out T? cacheResult);

    return Task.FromResult(cacheResult);
  }

  public Task SaveAsync<T>(string key, T value, TimeSpan expiry)
  {
    MemoryCacheEntryOptions options = new()
    {
      AbsoluteExpirationRelativeToNow = expiry
    };

    memoryCache.Set(key, value, options);

    logger.LogInformation("Data saved to in-memory cache , key: {key} expiry: {expiry}", key, expiry.TotalSeconds );

    return Task.CompletedTask;
  }
}
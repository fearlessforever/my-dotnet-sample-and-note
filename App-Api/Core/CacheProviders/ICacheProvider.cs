
namespace Fearlessforever.Api.Core.CacheProviders;

internal interface ICacheProvider
{
  Task SaveAsync<T>(string key, T value, TimeSpan expiry);
  Task<T?> GetAsync<T>(string key);
  Task<bool> DeleteAsync(string key);
}
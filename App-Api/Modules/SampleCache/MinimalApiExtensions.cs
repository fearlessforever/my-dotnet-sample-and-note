
using Fearlessforever.Api.Core.CacheProviders;
using Fearlessforever.Api.Modules.Starter;
using Microsoft.AspNetCore.Mvc;
using Fearlessforever.Api.Utils;

namespace Fearlessforever.Api.Modules.SampleCache;

public static partial class MinimalApiExtensions
{
  public static IEndpointConventionBuilder MapMiniApiSampleCache(this IEndpointRouteBuilder endpoints)
  {
    var sample = endpoints.MapGroup("SampleCache");
    {
      sample.MapGet("/", async ( [FromServices] CacheManager cacheManager ) =>
      {
        var data = await cacheManager.GetOrAddAsync("sample-cache-key-in-10-secs", async () =>
        {
          await Task.Delay(1000);
          string[] Summaries = ["Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"];
          var results = Enumerable.Range(1, 5).Select(index => new WeatherForecast
          {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
          }).ToArray();

          return results;

        }, TimeSpan.FromSeconds(10));

        return MyApiResponse.GenerateApiResponse(data: data);

      }).Produces<MyApiResponse<IEnumerable<WeatherForecast>>>(StatusCodes.Status200OK);
    }

    return sample;
  }
}
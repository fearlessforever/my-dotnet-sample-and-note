using Fearlessforever.Api.Controllers;
using Fearlessforever.Api.Modules.Starter;
using Fearlessforever.Api.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace Fearlessforever.Api.Modules.SampleRateLimiter;

[ApiController]
[Produces("application/json", "application/xml")]
[ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MyApiResponse<object>))]
// [EnableRateLimiting("concurrency-10")] // jika diaktifkan ini akan berlaku di semua route path yg di handle controller ini
[Route("[controller]")]
public class SampleRateLimiterController : ControllerBase
{
  private static readonly string[] Summaries = new[]
  {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

  private readonly ILogger<WeatherForecastController> _logger;

  public SampleRateLimiterController(ILogger<WeatherForecastController> logger)
  {
    _logger = logger;
  }

  [HttpGet]
  [Route("fixed-limit-10-in-10sec")]
  [EnableRateLimiting("fixed")]
  [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MyApiResponse<WeatherForecast[]>))]
  public IResult Get()
  {

    WeatherForecast[] results = Enumerable.Range(1, 5).Select(index => new WeatherForecast
    {
      Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
      TemperatureC = Random.Shared.Next(-20, 55),
      Summary = Summaries[Random.Shared.Next(Summaries.Length)]
    })
    .ToArray();

    return MyApiResponse.GenerateApiResponse(data: results);
  }
  
  [HttpGet]
  [EnableRateLimiting("concurrency-10")]
  [Route("concurrency-limit-10")]
  [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MyApiResponse<WeatherForecast[]>))]
  public IResult Get2()
  {

    WeatherForecast[] results = Enumerable.Range(1, 5).Select(index => new WeatherForecast
    {
      Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
      TemperatureC = Random.Shared.Next(-20, 55),
      Summary = Summaries[Random.Shared.Next(Summaries.Length)]
    })
    .ToArray();

    return MyApiResponse.GenerateApiResponse(data: results);
  }
}

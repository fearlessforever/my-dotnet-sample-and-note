
using Serilog;

namespace Fearlessforever.Api.Core.Logging;

public static partial class ApplicationServiceExtensions
{
  public static void AddCoreLogging(this IHostBuilder hostBuilder)
  {
    hostBuilder.UseSerilog((context, services, configuration) =>
    {
        configuration
          .ReadFrom.Configuration(context.Configuration)
          .ReadFrom.Services(services)
          .Enrich.FromLogContext()
          // .Enrich.With(services.GetRequiredService<HttpRequestAndCorrelationContextEnricher>())
          .WriteTo.Console()
          .WriteTo.File("Logs/log-.txt" , rollingInterval: RollingInterval.Day);
          // .WriteTo.Seq("http://localhost:5341");
    });
  }
}
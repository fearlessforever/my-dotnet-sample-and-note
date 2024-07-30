
using Fearlessforever.Shared.Configs;

namespace Fearlessforever.Api.Modules.SampleSignalR;

public static partial class MinimalApiExtensions
{
  public static IEndpointConventionBuilder MapMiniApiSampleSignalR(this IEndpointRouteBuilder endpoints , IConfiguration configuration)
  {
    FeaturesConfig featuresConfig = configuration.GetSection("Features").Get<FeaturesConfig>() ?? new();
    if (!featuresConfig.UseSignalR)
    {
      return endpoints.MapGroup("SampleSignalR");
    }
    
    // CancellationTokenSource cancellationTokenAllSse = new();
    var routeSampleSignalR = endpoints.MapGroup("SampleSignalR");
    {
      routeSampleSignalR.MapHub<SampleSignalrHub>("/real-time");
    }
    return routeSampleSignalR;
  }
}
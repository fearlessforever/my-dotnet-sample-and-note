
using Fearlessforever.Shared.Configs;

namespace Fearlessforever.Api.Core.SignalR;

public static partial class ApplicationServiceExtensions
{

  public static void AddCoreSignalR(this IServiceCollection services, IConfiguration configuration)
  {
    FeaturesConfig featuresConfig = configuration.GetSection("Features").Get<FeaturesConfig>() ?? new();
    if (featuresConfig.UseSignalR)
    {
      services.AddSignalR();
    }
  }
}
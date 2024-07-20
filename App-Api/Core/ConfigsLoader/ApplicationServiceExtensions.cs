
using Fearlessforever.Shared.Utils;

namespace Fearlessforever.Api.Core.ConfigsLoader;

public static partial class ApplicationServiceExtensions
{
  public static IConfigurationBuilder AddCoreConfigsLoader(this IConfigurationBuilder configuration, string currentEnvironmentName)
  {
    configuration.SetBasePath(Directory.GetCurrentDirectory())
      .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
      .AddJsonFile($"appsettings.{currentEnvironmentName}.json", optional: true, reloadOnChange: true)
      .AddUserSecrets<IMyWebApi>()
      .AddEnvironmentVariables();

    return configuration;
  }

  public static void UseCoreConfigsLogHelper(this IHost app)
  {
    var Logger = app.Services.GetRequiredService<ILogger<object>>();
    DebugHelper.Logger = Logger;
  }
}
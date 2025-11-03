
namespace Fearlessforever.Api.Modules.Sample;

public static partial class ApplicationServiceExtensions
{
  public static void AddModuleSampleServices(this IServiceCollection services)
  {
    services.AddSingleton<SampleService>();
  }
}

namespace Fearlessforever.Api.Modules.SampleQueue;

public static partial class ApplicationServiceExtensions
{
  public static void AddModuleSampleQueueServices(this IServiceCollection services)
  {
    services.AddScoped<ISampleQueueService,SampleQueueService>();
  }
}
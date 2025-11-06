
using Fearlessforever.Api.Modules.Sample;
using Fearlessforever.Api.Modules.SampleCache;
using Fearlessforever.Api.Modules.SampleQueue;
using Fearlessforever.Api.Modules.SampleSse;

namespace Fearlessforever.Api.Core.Routes;

public static partial class ApplicationServiceExtensions
{
  public static void UseCoreRoutesMinimalApi(this IEndpointRouteBuilder appEndpoints)
  {
    RouteGroupBuilder RoutesMinimalApi = appEndpoints.MapGroup("v1");
    RoutesMinimalApi.MapSwagger();
    //============================================================
    RoutesMinimalApi.MapMiniApiSample();
    RoutesMinimalApi.MapMiniApiSampleCache();
    RoutesMinimalApi.MapMiniApiSampleQueue();
    RoutesMinimalApi.MapMiniApiSampleSse();
  }
}
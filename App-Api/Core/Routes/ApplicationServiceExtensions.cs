
using Fearlessforever.Api.Modules.Sample;
using Fearlessforever.Api.Modules.SampleCache;

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
  }
}
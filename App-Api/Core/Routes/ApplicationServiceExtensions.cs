
using Fearlessforever.Api.Modules.Sample;
using Fearlessforever.Api.Modules.SampleCache;
using Fearlessforever.Api.Modules.SampleQueue;
using Fearlessforever.Api.Modules.SampleSignalR;
using Fearlessforever.Api.Modules.SampleSse;
using static Fearlessforever.Api.Utils.MyApiResponse;

namespace Fearlessforever.Api.Core.Routes;

public static partial class ApplicationServiceExtensions
{
  public static void UseCoreRoutesMinimalApi(this IEndpointRouteBuilder appEndpoints , IConfiguration configuration)
  {
    RouteGroupBuilder RoutesMinimalApi = appEndpoints.MapGroup("v1");
    RoutesMinimalApi.MapSwagger();
    //============================================================
    RoutesMinimalApi.MapMiniApiSample();
    RoutesMinimalApi.MapMiniApiSampleCache();
    RoutesMinimalApi.MapMiniApiSampleQueue();
    RoutesMinimalApi.MapMiniApiSampleSse();
    RoutesMinimalApi.MapMiniApiSampleSignalR(configuration);

    RoutesMinimalApi.MapFallback((HttpContext httpContext) =>
    {
      return GenerateApiResponse<object>(null, code: StatusCodes.Status404NotFound, isHeaderStatus: true , status:"error" , message:"Route Handler For This: Not Found");
    });
  }
}
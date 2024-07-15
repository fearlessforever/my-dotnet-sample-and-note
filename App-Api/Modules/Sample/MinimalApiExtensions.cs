
using Microsoft.AspNetCore.Mvc;
using static Fearlessforever.Api.Utils.MyApiResponse;

namespace Fearlessforever.Api.Modules.Sample;

public static partial class MinimalApiExtensions
{
  public static IEndpointConventionBuilder MapMiniApiSample(this IEndpointRouteBuilder endpoints)
  {
    var sample = endpoints.MapGroup("Sample");
    {
      sample.MapGet("/", ([FromServices]SampleService sampleService) =>
      {
        return sampleService.GetAll();
      });

      sample.MapPost("/", ( SampleDtoCreate sampleDtoCreate , [FromServices]SampleService sampleService) =>
      {
        return sampleService.Add(sampleDtoCreate);
      });

      sample.MapGet("/{Id}", ( int Id , [FromServices]SampleService sampleService) =>
      {
        SampleDtoRequiredId sampleDtoRequiredId = new() { Id = Id };
        return sampleService.GetById(sampleDtoRequiredId);
      });

      sample.MapPut("/{Id}", ([FromRoute] int Id , [FromBody] SampleDtoUpdate sampleDtoUpdate , [FromServices]SampleService sampleService) =>
      {
        sampleDtoUpdate.Id = Id;
        return sampleService.Update(sampleDtoUpdate);
      });

      sample.MapDelete("/{Id}", ([FromRoute] int Id , [FromServices]SampleService sampleService) =>
      {
        SampleDtoRequiredId sampleDtoRequiredId = new() { Id = Id };
        return sampleService.DeleteById(sampleDtoRequiredId);
      });

      // Fallback unknown path
      sample.MapFallback((HttpContext httpContext) =>
      {
        return GenerateApiResponse<object>(data: null, isHeaderStatus: true, code: 404, status: "error", message: "Not Found");
      });

      // Custom middleware handling for this certain routes group
      sample.AddEndpointFilter( async ( context , next )=>
      {
        object? result;

        try
        {
          result = await next(context);
        }
        catch (Exception error)
        {
          result = GenerateApiResponse<object>(data: null, isHeaderStatus: true, code: 402, status: "error", message: error.Message );
        }

        return result;
      });
    }

    return sample;
  }
}
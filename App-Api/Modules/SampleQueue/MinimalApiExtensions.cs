using System.ComponentModel.DataAnnotations;
using Fearlessforever.Api.Utils;
using Microsoft.AspNetCore.Mvc;

namespace Fearlessforever.Api.Modules.SampleQueue;

public static partial class MinimalApiExtensions
{
  public static IEndpointConventionBuilder MapMiniApiSampleQueue(this IEndpointRouteBuilder endpoints)
  {
    var sampleQueue = endpoints.MapGroup("SampleQueue");
    {
      sampleQueue.MapPost("/", ([FromBody] SampleQueueDto sampleQueueDto) =>
      {
        if (string.IsNullOrEmpty(sampleQueueDto.Message))
        { 
          return MyApiResponse.GenerateApiResponse<object>(null , message:"Empty Message" ,code:400 , isHeaderStatus:true);
        }
        
        SampleQueueService.DispatchJob(sampleQueueDto.Message);
        return MyApiResponse.GenerateApiResponse<object>(null);
      }).Produces<MyApiResponse<object>>();
    }

    return sampleQueue;
  }
}

internal sealed record SampleQueueDto
{
  [Required]
  public string Message { get; set; } = string.Empty;
}
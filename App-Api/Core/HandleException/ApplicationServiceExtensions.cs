
using Fearlessforever.Api.Utils;
using Microsoft.AspNetCore.Diagnostics;

namespace Fearlessforever.Api.Core.HandleException;

public static partial class ApplicationServiceExtensions
{
  public static void AddCoreHandleException(this IServiceCollection services)
  {
    services.AddProblemDetails(configure =>
    {
      configure.CustomizeProblemDetails = context =>
      {
        context.ProblemDetails.Extensions.TryAdd("requestId", context.HttpContext.TraceIdentifier);
      };
    });

#if NET8_0_OR_GREATER
    services.AddExceptionHandler<MyExceptionHandler>();
#endif
  }

  public static void UseCoreHandleException(this IApplicationBuilder applicationBuilder)
  {
    applicationBuilder.UseExceptionHandler(builder=>
    {
#if NET7_0

      builder.Run(async context =>
      {
        Exception? readException = context.Features.Get<IExceptionHandlerPathFeature>()?.Error;
        var logger = context.RequestServices.GetService<ILogger<object>>();
        var result = new MyApiResponse
        {
          Code = StatusCodes.Status500InternalServerError,
          Message = readException?.Message ?? "Unhandle Error has occured",
          Status = "error",
        };

        logger?.LogError("Unhandle Error : {error} " , result );

        await context.Response.WriteAsJsonAsync(result);

        // var problemDetailService = context.RequestServices.GetService<IProblemDetailsService>();
        // if (problemDetailService != null)
        // {
        //   await problemDetailService.WriteAsync(new ProblemDetailsContext
        //   {
        //     HttpContext = context,
        //     ProblemDetails = new Microsoft.AspNetCore.Mvc.ProblemDetails
        //     {
        //       Type = readException?.GetType().Name ?? "Unknown error source",
        //       Title = "an error occured",
        //       Detail = readException?.Message ?? "Unhandle Error has occured",
        //     }
        //   });
        // }
        
      });
        
#endif
    });
  }
}
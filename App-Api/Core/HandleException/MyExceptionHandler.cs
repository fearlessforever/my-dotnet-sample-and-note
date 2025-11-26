
using Fearlessforever.Api.Utils;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Fearlessforever.Api.Core.HandleException;

internal sealed class MyExceptionHandler(
  //  IProblemDetailsService problemDetailsService 
  ILogger<MyExceptionHandler> logger
): IExceptionHandler
{
  public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
  {
    var result = new MyApiResponse
    {
      Code = StatusCodes.Status500InternalServerError,
      Message = exception.Message,
      Status = "error",
    };
    
    result.Message = exception.Message;
    ICollection<string> errors = [];

    if (!string.IsNullOrEmpty(exception.InnerException?.Message) && exception.Message != exception.InnerException.Message)
        errors.Add(exception.InnerException.Message);

    result.Errors = errors;

    logger.LogError("Unhandle Error : {error} " , result );

    await httpContext.Response.WriteAsJsonAsync(result, cancellationToken);
    return true;
    
    // return await problemDetailsService.TryWriteAsync(new ProblemDetailsContext
    // {
    //   HttpContext = httpContext,
    //   Exception = exception,
    //   ProblemDetails =  new ProblemDetails
    //   {
    //     Type = exception.GetType().Name,
    //     Title = "an error occured",
    //     Detail = exception.Message ,
    //   }
    // } );
  }
}

using Fearlessforever.Api.Core.AppCancelToken;
using Fearlessforever.Api.Utils;
using Fearlessforever.Shared.Utils;

namespace Fearlessforever.Api.Modules.SampleSse;

public static partial class MinimalApiExtensions
{
  public static IEndpointConventionBuilder MapMiniApiSampleSse(this IEndpointRouteBuilder endpoints)
  {
    CancellationTokenSource cancellationTokenAllSse = new();
    var routeSampleSse = endpoints.MapGroup("SampleSse");
    {
      routeSampleSse.MapGet("/", async ( httpContext ) =>
      {
        // var linkedCancelToken = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken,AppCancelTokenService.CancelTokenSource.Token);
        var linkedCancelToken = CancellationTokenSource.CreateLinkedTokenSource(httpContext.RequestAborted, AppCancelTokenService.CancelTokenSource.Token, cancellationTokenAllSse.Token);
        try
        {
          httpContext.Response.Headers.Append("Content-Type", "text/event-stream");
          while (!linkedCancelToken.IsCancellationRequested)
          {
            // var data = DateTime.Now.ToString();
            var data = new MyApiResponse
            {
              Message = $"Hello from SSE! {DateTime.UtcNow}"
            };
            await httpContext.Response.WriteAsync($"data: {data}\n\n", linkedCancelToken.Token);
            await Task.Delay(1000, linkedCancelToken.Token); // Send every second
          }
        }
        catch (OperationCanceledException)
        {
          DebugHelper.Log("Server Sent Event Cancelled!", "Minimal Api SSE");
        }

      });

      routeSampleSse.MapDelete("/", () =>
      {
        cancellationTokenAllSse.Cancel();
        cancellationTokenAllSse = new(); // generate new cancelation token source after cancel

        return MyApiResponse.GenerateApiResponse<object>(null);
      }).Produces<MyApiResponse<object>>();
    }
    return routeSampleSse;
  }
}
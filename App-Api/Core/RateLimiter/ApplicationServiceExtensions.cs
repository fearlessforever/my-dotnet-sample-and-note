
using System.Threading.RateLimiting;
using Microsoft.AspNetCore.RateLimiting;

namespace Fearlessforever.Api.Core.RateLimiter;

public static partial class ApplicationServiceExtensions
{
  public static void AddCoreRateLimiter(this IServiceCollection services)
  {
    services.AddRateLimiter(options =>
    {
      options.AddConcurrencyLimiter("concurrency-10", opt =>
      {
        opt.PermitLimit = 10; // Max requests
        opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        opt.QueueLimit = 0; // 0 = No queueing for this example
      });

      options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

      options.AddFixedWindowLimiter("fixed", opt =>
      {
        opt.PermitLimit = 10; // Max requests
        opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        opt.QueueLimit = 0; // 0 = No queueing for this example
        opt.Window = TimeSpan.FromSeconds(10); // Time window
      });

    });
  }

  public static void UseCoreRateLimiter(this IApplicationBuilder applicationBuilder)
  {
    applicationBuilder.UseRateLimiter();
  }
}

using Fearlessforever.Shared.Utils;

namespace Fearlessforever.Api.Core.AppCancelToken;

public static partial class ApplicationServiceExtensions
{

  // public static void UseCoreAppCancelToken(this IHost app)
  public static void UseCoreAppCancelToken(this WebApplication app)
  {
    IHostApplicationLifetime appLifetTime = app.Services.GetRequiredService<IHostApplicationLifetime>();

    AppCancelTokenService.RegisterCancelToken(appLifetTime);
    app.UseMiddleware<CancelTokenProviderMiddleware>();
  }

  public static void AddCoreAppCancelTokenProvider(this IServiceCollection services)
  {
    Console.Title = "My .NET Note & Sample Project";
    services.AddScoped<CancelTokenProviderMiddleware>();
    services.AddScoped<CancelTokenProvider>();
  }
}
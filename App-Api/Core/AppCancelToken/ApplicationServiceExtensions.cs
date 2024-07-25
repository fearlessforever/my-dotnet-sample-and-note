
namespace Fearlessforever.Api.Core.AppCancelToken;

public static partial class ApplicationServiceExtensions
{
  
  public static void UseCoreAppCancelToken(this IHost app)
  {
    Console.Title = "My .NET Note & Sample Project";
    IHostApplicationLifetime appLifetTime = app.Services.GetRequiredService<IHostApplicationLifetime>();

    AppCancelTokenService.RegisterCancelToken(appLifetTime);
  }
}
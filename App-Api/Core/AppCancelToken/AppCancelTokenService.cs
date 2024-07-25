using Fearlessforever.Shared.Utils;

namespace Fearlessforever.Api.Core.AppCancelToken;

public static class AppCancelTokenService
{
  // public static IHostApplicationLifetime? HostApplicationLifetime { get; set; }
  public static readonly CancellationTokenSource CancelTokenSource = new();
  private static bool IsSet { get; set; } = false;

  public static void RegisterCancelToken(IHostApplicationLifetime applicationLifetime)
  {
    if (IsSet) return;

    static void OnApplicationStopping()
    {
      DebugHelper.Log("Application is stopping. Signalling cancellation", "Application Cancel Token");
      CancelTokenSource.Cancel();
    }

    applicationLifetime.ApplicationStopping.Register(OnApplicationStopping);
    IsSet = true;
  }
}
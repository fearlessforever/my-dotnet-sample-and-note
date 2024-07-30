
using Fearlessforever.Api.Core.AppCancelToken;
using Fearlessforever.Shared.Utils;
using Microsoft.AspNetCore.SignalR;

namespace Fearlessforever.Api.Modules.SampleSignalR;

public static class ConnectedClients
{
  public static readonly HashSet<string> Ids = [];

  private static bool IsRunningBroadCastEach1Sec = false;
  private static CancellationTokenSource cancellationTokenSource = new();

  public static IHubCallerClients? Clients { get; set; }

  public static async Task RunningBroadCastAsync()
  {

    if (!IsRunningBroadCastEach1Sec && Clients != null)
    {
      IsRunningBroadCastEach1Sec = true;
      var linkedCancelToken = CancellationTokenSource.CreateLinkedTokenSource(cancellationTokenSource.Token, AppCancelTokenService.CancelTokenSource.Token);

      try
      {
        while (!linkedCancelToken.IsCancellationRequested)
        {
          await Clients.All.SendAsync("BroadcastMessage", $"Hello From SignalR: {DateTime.UtcNow}", cancellationToken: linkedCancelToken.Token);
          await Task.Delay(17000, linkedCancelToken.Token);
          DebugHelper.Log("Broadcasting .....", "SignarR Broadcast");
          RemoveBroadCastIfNoneConnectedUser();
        }
      }
      catch (OperationCanceledException)
      {
        DebugHelper.LogError("Has been Cancelled", "SignarR Broadcast");
      }
    }
  }

  public static void RemoveBroadCastIfNoneConnectedUser()
  { 
    if (Ids.Count < 1)
    {
      cancellationTokenSource.Cancel();
      cancellationTokenSource = new();
      IsRunningBroadCastEach1Sec = false;
    }
  }
}
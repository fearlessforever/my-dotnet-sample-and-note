using Microsoft.AspNetCore.SignalR;

namespace Fearlessforever.Api.Modules.SampleSignalR;

internal sealed class SampleSignalrHub(ILogger<SampleSignalrHub> logger ) : Hub
{
  public async Task SendMessage(string message)
  {
    var connectionId = Context.ConnectionId;
    logger.LogInformation("Message received: {_0}", message);
    // await Clients.All.SendAsync("ReceiveMessage", message);
    await Clients.Others.SendAsync("ReceiveMessage", $"From ConnectionId ({connectionId}) : {message}" , Context.ConnectionAborted );
  }

  public override Task OnDisconnectedAsync(Exception? exception)
  {
    ConnectedClients.Ids.Remove(Context.ConnectionId);

    if (ConnectedClients.Ids.Count < 1)
    {
      ConnectedClients.RemoveBroadCastIfNoneConnectedUser();
      logger.LogError("{_0} : No Client Connected", "[SignarR Broadcast]");
    }
    logger.LogInformation("{_0} : A Client Has been Disconnected", "[SignarR Broadcast]");

    return Task.CompletedTask;
  }

  public override async Task OnConnectedAsync()
  {
    await Task.Yield();
    var connectionId = Context.ConnectionId;

    ConnectedClients.Clients ??= Clients;
    ConnectedClients.Ids.Add(connectionId);
    
    _ = ConnectedClients.RunningBroadCastAsync();

    // You can use the connection ID to send a message to this specific client
    _ = Clients.Client(connectionId).SendAsync("ReceiveMessage", $"Your connection ID is: {connectionId}", cancellationToken: Context.ConnectionAborted);

    _ = Clients.All.SendAsync("BroadcastMessage", $"New Connection Id: {connectionId}", cancellationToken: Context.ConnectionAborted);
    
  }
}
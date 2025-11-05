
using Fearlessforever.Shared.Utils;
using Hangfire;

namespace Fearlessforever.Api.Modules.SampleQueue;

public interface ISampleQueueService
{ 
  [JobDisplayName("Sample Queue Job: {1} - {0}")]
  Task ExecuteAsync( string message , DateTime dateTime);
}
public class SampleQueueService : ISampleQueueService
{
  public static void DispatchJob( string message )
  {
    BackgroundJob.Enqueue<ISampleQueueService>(x => x.ExecuteAsync( message , DateTime.UtcNow ) );
  }

  public async Task ExecuteAsync(string message, DateTime dateTime)
  {
    var delay = Random.Shared.Next(3,5);
    await Task.Delay(delay * 1000);
    DebugHelper.Log($"Task Finished in {delay} seconds. Message: {message} Time: {dateTime}" , "Background Task / Scheduler");
  }
}
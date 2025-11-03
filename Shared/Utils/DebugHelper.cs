using System.Text.Json;

namespace Fearlessforever.Shared.Utils;

public static class DebugHelper
{
  public static void Log(object? data)
  {

    Console.WriteLine($"Debug : {JsonSerializer.Serialize(data)}");
  }

  public static string ToString(object data)
  {
    return JsonSerializer.Serialize(data);
  }
}
using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace Fearlessforever.Shared.Utils;

public static class DebugHelper
{
  public static ILogger<object>? Logger { get; set; }
  public static void Log(object? data , string? name = "")
  {
    if (!string.IsNullOrEmpty(name))
    {
      name = $"[{name}]";
    }

    if (Logger != null)
      Logger.LogInformation("Debug {name} : {Debug}", name, JsonSerializer.Serialize(data));
    else
      Console.WriteLine($"Debug {name} : {JsonSerializer.Serialize(data)}");
  }

  public static void LogError(object? data , string? name = "")
  {
    if (!string.IsNullOrEmpty(name))
    {
      name = $"[{name}]";
    }
    
    if (Logger != null)
      Logger.LogError("Error {name} : {Error}", name, JsonSerializer.Serialize(data));
    else
      Console.WriteLine($"Error {name} : {JsonSerializer.Serialize(data)}");
  }

  public static string ToString(object data)
  {
    return JsonSerializer.Serialize(data);
  }
}

namespace Fearlessforever.Shared.Configs;

public class HangfireConfigs
{ 
  public string DatabaseType { get; set; } = string.Empty;
  public string Connection { get; set; } = string.Empty;
  public bool UseHangfireDashboard { get; set; } = false;
  public string HangfireDashboardPath { get; set; } = "/hangfire";
}
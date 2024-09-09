
namespace Fearlessforever.Databases.Shared.Models.Testing;

public class ViewPostgreKeyItem : BaseEntitySoftDelete<string>
{
  public string Source { get; set; } = string.Empty;
  public string Name { get; set; } = string.Empty;
  
}
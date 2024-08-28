
namespace Fearlessforever.Databases.Shared.Models.Testing;

public class ViewSqliteKeyItem : BaseEntitySoftDelete<string>
{
  public string Source { get; set; } = string.Empty;
  public string Name { get; set; } = string.Empty;
  
}
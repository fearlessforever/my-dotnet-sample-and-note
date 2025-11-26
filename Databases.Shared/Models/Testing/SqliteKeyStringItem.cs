namespace Fearlessforever.Databases.Shared.Models.Testing;
public class SqliteKeyStringItem : BaseEntitySoftDelete<string>
{ 
  public required string Name { get; set; }
}
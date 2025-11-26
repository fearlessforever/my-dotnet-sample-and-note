namespace Fearlessforever.Databases.Shared.Models.Testing;
public class SqliteKeyGuidItem : BaseEntitySoftDelete<Guid>
{ 
  public required string Name { get; set; }
}
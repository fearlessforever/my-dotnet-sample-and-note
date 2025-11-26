namespace Fearlessforever.Databases.Shared.Models.Testing;
public class SqliteKeyByteItem : BaseEntitySoftDelete<byte[]>
{ 
  public required string Name { get; set; }
}
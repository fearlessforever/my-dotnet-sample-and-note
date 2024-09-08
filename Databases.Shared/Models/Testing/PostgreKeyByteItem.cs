namespace Fearlessforever.Databases.Shared.Models.Testing;
public class PostgreKeyByteItem : BaseEntitySoftDelete<byte[]>
{ 
  public required string Name { get; set; }
}
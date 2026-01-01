namespace Fearlessforever.Databases.Shared.Models.Testing;
public class InMemoryKeyByteItem : BaseEntitySoftDelete<byte[]>
{ 
  public required string Name { get; set; }
}
namespace Fearlessforever.Databases.Shared.Models.Testing;
public class MsSqlKeyByteItem : BaseEntitySoftDelete<byte[]>
{ 
  public required string Name { get; set; }
}
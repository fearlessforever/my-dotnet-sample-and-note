namespace Fearlessforever.Databases.Shared.Models.Testing;
public class PostgreKeyGuidItem : BaseEntitySoftDelete<Guid>
{ 
  public required string Name { get; set; }
}
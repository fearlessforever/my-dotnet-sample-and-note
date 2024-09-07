namespace Fearlessforever.Databases.Shared.Models.Testing;
public class InMemoryKeyGuidItem : BaseEntitySoftDelete<Guid>
{ 
  public required string Name { get; set; }
}
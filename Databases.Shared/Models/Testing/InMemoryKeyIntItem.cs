namespace Fearlessforever.Databases.Shared.Models.Testing;
public class InMemoryKeyIntItem : BaseEntitySoftDelete<int>
{ 
  public required string Name { get; set; }
}
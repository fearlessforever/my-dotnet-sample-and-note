namespace Fearlessforever.Databases.Shared.Models.Testing;
public class MsSqlKeyGuidItem : BaseEntitySoftDelete<Guid>
{ 
  public required string Name { get; set; }
}
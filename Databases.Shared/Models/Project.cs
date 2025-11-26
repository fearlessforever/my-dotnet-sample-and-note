namespace Fearlessforever.Databases.Shared.Models;

public class Project : BaseEntitySoftDelete<int>
{
  public required string Name { get; set; }

  public string Description { get; set; } = string.Empty;

  public ICollection<Ticket> Tickets { get; set; } = [];
}